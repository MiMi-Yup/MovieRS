using AutoMapper;
using Microsoft.ML;
using Microsoft.ML.Data;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class RecommendRepository : IRecommendRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MLContext _mlContext;
        private readonly DataViewSchema _modelSchema;
        private readonly ITransformer _model;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public RecommendRepository(
            MovieRsContext context,
            ILogger logger,
            IMapper mapper,
            IConfiguration configuration,
            IMovieRepository movieRepository,
            MLContext mlContext
            )
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _mlContext = mlContext;
            _model = _mlContext.Model.Load(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _configuration["ModelUMR"])).FullName, out _modelSchema);
        }

        public async Task<IList<TMDbLib.Objects.Movies.Movie>> GetUserMovieRecommendation(MovieRS.API.Models.User user, int takeMax = 0)
        {
            List<Movie> movie = (await _movieRepository.GetLocalVideos()).Where(item => item.IdTmdb != null).ToList();
            IDataView predictPool = _model.Transform(_mlContext.Data.LoadFromEnumerable<InputPredictModel>(movie.Select(item => new InputPredictModel { userId = user.Id, movieId = item.Id })));
            IEnumerable<int> movieIds = predictPool.GetColumn<int>("movieId");
            IEnumerable<int?> top10MovieIds = predictPool.GetColumn<float>("Score")
                .Select((item, index) => new { index = index, Score = item })
                .OrderByDescending(item => item.Score)
                .Take(takeMax > 0 ? takeMax + 10 : 10)
                .Select(item => movieIds.ElementAtOrDefault(item.index))
                .Select(item => movie.Find(_item => _item.Id == item))
                .Select(item => item?.IdTmdb)
                .Where(item => item != null);
            TMDbLib.Objects.Movies.Movie[] movies = await Task.WhenAll(top10MovieIds.Select(item => _movieRepository.GetMovie(item.Value)));
            IEnumerable<TMDbLib.Objects.Movies.Movie> filter = movies.Where(item => item != null);
            if (takeMax > 0)
                filter = filter.Take(takeMax);
            return filter.ToList();
        }

        public async Task<TMDbLib.Objects.Movies.Movie[]> GetMovieGenreRecommendation()
        {
            return null;
        }

        private class InputPredictModel
        {
            public int userId;
            public int movieId;
        }

        private class OutputPredictModel
        {
            public float Label;
            public float Score;
        }
    }
}
