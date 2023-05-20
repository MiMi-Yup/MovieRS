using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IHistoryMovieRepository _historyMovieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly Models.RsContext _rsContext;

        public RecommendRepository(
            ILogger logger,
            IMapper mapper,
            IConfiguration configuration,
            IMovieRepository movieRepository,
            IHistoryMovieRepository historyMovieRepository,
            IReviewRepository reviewRepository,
            MLContext mlContext,
            Models.RsContext rsContext
            )
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _historyMovieRepository = historyMovieRepository;
            _reviewRepository = reviewRepository;
            _rsContext = rsContext;

            _mlContext = mlContext;
            _modelUMR = _mlContext.Model.Load(new FileInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", _configuration["ModelUMR"])).FullName, out _modelSchemaUMR);
        }

        private const int MAX_ITEM_OF_PAGE = 10;

        public async Task<ResultRecommendation> GetUserMovieRecommendation(Models.User user, int takeMax = 0)
        {
            Models.PreTraining? hasTrained = await _rsContext.PreTrainings.FirstOrDefaultAsync(item => item.UserId == user.Id);
            takeMax = takeMax > 0 ? takeMax : MAX_ITEM_OF_PAGE;
            if (hasTrained == null)
            {
                var histories = await _historyMovieRepository.GetHistories(user);
                var review = await _reviewRepository.GetReviews(user, take: 50);
                var filterReview = await Task.WhenAll(review.Results.Where(item => item.Rating.HasValue && item.Rating >= 3.5).Select(item => _movieRepository.GetRecommendation(item.Movie.Id)));
                var rsReview = filterReview.Where(item => item != null).SelectMany(item => item!.Results);

                var filterHistory = await Task.WhenAll(histories.Results.Take(10).Where(item => item.Movie != null).Select(item => _movieRepository.GetRecommendation(item.Movie.Id)));
                var rsHistory = filterHistory.Where(item => item != null).SelectMany(item => item!.Results);

                rsReview.ToList().AddRange(rsHistory);
                var result = rsReview.DistinctBy(item => item.Id).OrderByDescending(item => item.VoteAverage).Where(item => item.VoteCount > 10).Take(takeMax);

                if (result.Count() > 0)
                {
                    //content-base
                    return new ResultRecommendation
                    {
                        UserId = user.Id,
                        Results = result.Select(item => new Recommendation
                        {
                            Score = 2.5f,
                            Movie = item
                        }).ToList()
                    };
                }
                else
                {
                    //popular
                    result = (await _movieRepository.GetPopular(take: takeMax))?.Results;
                    return new ResultRecommendation
                    {
                        UserId = user.Id,
                        Results = result.Select(item => new Recommendation
                        {
                            Score = 0,
                            Movie = item
                        }).ToList()
                    };
                }
            }
            else
            {
                //collaborative filtering
                var moviesRepo = _rsContext.PreMovies;
                IDataView predictPool = _modelUMR.Transform(_mlContext.Data.LoadFromEnumerable(moviesRepo.Select(item => new InputMoviePredictModel
                {
                    userId = user.Id,
                    movieId = (int)item.MovieId
                })));
                var movieIds = predictPool.GetColumn<int>("movieId").ToArray();
                var top10MovieIds = predictPool.GetColumn<float>("Score")
                    .Select((item, index) => new { index = index, Score = item })
                    .OrderByDescending(item => item.Score)
                    .Where(item => item.Score>= 3.5 && item.Score <= 4.5)
                    .Take(takeMax)
                    .Select(item => new { tmdbId = moviesRepo.Find((long)movieIds[item.index])?.TmdbId, Score = item.Score })
                    .Where(item => item.tmdbId != null).ToList();
                Recommendation[] movies = await Task.WhenAll(top10MovieIds.Select(async item => new Recommendation
                {
                    Score = item.Score,
                    Movie = await _movieRepository.GetMovieBy3rd((int)item.tmdbId!.Value)
                }));
                IEnumerable<Recommendation> filter = movies.Where(item => !float.IsNaN(item.Score) && item.Movie != null);
                if (takeMax > 0)
                    filter = filter.Take(takeMax);
                return new ResultRecommendation
                {
                    UserId = user.Id,
                    Results = filter.ToList(),
                };
            }
        }

        private class InputMoviePredictModel
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
