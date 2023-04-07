using AutoMapper;
using Microsoft.ML;
using Microsoft.ML.Data;
using MovieRS.API.Core.Contracts;

namespace MovieRS.API.Core.Repositories
{
    public class RecommendRepository : IRecommendRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MLContext _mlContext;
        private readonly DataViewSchema _modelSchemaUMR;
        private readonly ITransformer _modelUMR;
        private readonly DataViewSchema _modelSchemaMGR;
        private readonly ITransformer _modelMGR;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IHistoryMovieRepository _historyMovieRepository;
        private readonly IFavouriteRepository _favouriteRepository;

        public RecommendRepository(
            ILogger logger,
            IMapper mapper,
            IConfiguration configuration,
            IMovieRepository movieRepository,
            IHistoryMovieRepository historyMovieRepository,
            IFavouriteRepository favouriteRepository,
            MLContext mlContext
            )
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _historyMovieRepository = historyMovieRepository;
            _favouriteRepository = favouriteRepository;

            _mlContext = mlContext;
            _modelUMR = _mlContext.Model.Load(new FileInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", _configuration["ModelUMR"])).FullName, out _modelSchemaUMR);
            _modelMGR = _mlContext.Model.Load(new FileInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", _configuration["ModelMGR"])).FullName, out _modelSchemaMGR);
        }

        public async Task<ResultRecommendation> GetUserMovieRecommendation(Models.User user, int takeMax = 0)
        {
            List<Models.Movie> moviesRepo = (await _movieRepository.GetLocalVideos()).Where(item => item.IdTmdb != null).ToList();
            IDataView predictPool = _modelUMR.Transform(_mlContext.Data.LoadFromEnumerable<InputMoviePredictModel>(moviesRepo.Select(item => new InputMoviePredictModel { userId = user.Id, movieId = item.Id })));
            IEnumerable<int> movieIds = predictPool.GetColumn<int>("movieId");
            IEnumerable<Task<Recommendation>> top10MovieIds = predictPool.GetColumn<float>("Score")
                .Select((item, index) => new { index = index, Score = item })
                .OrderByDescending(item => item.Score)
                .Take(takeMax > 0 ? takeMax + 10 : 10)
                .Select(item => new { tmdbId = moviesRepo.Find(_item => _item.Id == movieIds.ElementAtOrDefault(item.index))?.IdTmdb, Score = item.Score })
                .Where(item => item.tmdbId != null)
                .Select(async item => new Recommendation { Score = item.Score, Movie = await _movieRepository.GetMovie(item.tmdbId.Value) });
            Recommendation[] movies = await Task.WhenAll(top10MovieIds);
            IEnumerable<Recommendation> filter = movies.Where(item => !float.IsNaN(item.Score) && item.Movie != null);
            if (takeMax > 0)
                filter = filter.Take(takeMax);
            return new ResultRecommendation
            {
                UserId = user.Id,
                Results = filter.ToList(),
            };
        }

        public async Task<ResultRecommendation> GetMovieGenreRecommendation(Models.User user, int takeMax = 0)
        {
            List<Models.Movie> moviesRepo = (await _movieRepository.GetLocalVideos()).Where(item => item.IdTmdb != null).ToList();
            TMDbLib.Objects.General.SearchContainer<HistoryMovie> latestHistories = await _historyMovieRepository.GetHistories(user);
            IEnumerable<string> genres = latestHistories.Results.SelectMany(item => item.Movie.Genres).DistinctBy(item => item.Id).Select(item => item.Name);
            IDataView predictPool = _modelMGR.Transform(_mlContext.Data.LoadFromEnumerable<InputGenrePredictModel>(moviesRepo.SelectMany(item => genres.Select(_item => new InputGenrePredictModel { movieId = item.Id, genre = _item }))));
            IEnumerable<int> movieIds = predictPool.GetColumn<int>("movieId");
            IEnumerable<Task<Recommendation>> top10MovieIds = predictPool.GetColumn<float>("Score")
                .Select((item, index) => new { index = index, Score = item })
                .OrderByDescending(item => item.Score)
                .Take(takeMax > 0 ? takeMax + 10 : 10)
                .Select(item => new { tmdbId = moviesRepo.Find(_item => _item.Id == movieIds.ElementAtOrDefault(item.index))?.IdTmdb, Score = item.Score })
                .Where(item => item.tmdbId != null)
                .Select(async item => new Recommendation { Score = item.Score, Movie = await _movieRepository.GetMovie(item.tmdbId.Value) });
            Recommendation[] movies = await Task.WhenAll(top10MovieIds);
            IEnumerable<Recommendation> filter = movies.Where(item => !float.IsNaN(item.Score) && item.Movie != null);
            if (takeMax > 0)
                filter = filter.Take(takeMax);
            return new ResultRecommendation
            {
                UserId = user.Id,
                Results = filter.ToList(),
            };
        }

        private class InputMoviePredictModel
        {
            public int userId;
            public int movieId;
        }

        private class InputGenrePredictModel
        {
            public int movieId;
            public string genre;
        }

        private class OutputPredictModel
        {
            public float Label;
            public float Score;
        }
    }
}
