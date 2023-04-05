using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class RecommendRepository : GenericRepository<Movie>, IRecommendRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MLContext _mlContext;
        private readonly DataViewSchema _modelSchema;
        private readonly ITransformer _model;
        private readonly IUnitOfWork _unitOfWork;

        public RecommendRepository(MovieRsContext context, ILogger logger, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration) : base(context, logger, mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;

            _mlContext = new MLContext();
            _model = _mlContext.Model.Load(new FileInfo(Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName, _configuration["Model"])).FullName, out _modelSchema);
        }

        public Task<IList<Movie>> GetRecommends()
        {
            return Task.Factory.StartNew<IList<Movie>>(() =>
            {
                //get list movie in model (IdTmdb in database)
                IList<Movie> result = null;
                IEnumerable<OutputPredictModel> top5Predicts = result
                    .Select(item => new InputPredictModel { userId = 1, movieId = item.IdTmdb.Value })
                    .Select(item => _mlContext.Model.CreatePredictionEngine<InputPredictModel, OutputPredictModel>(_model).Predict(item))
                    .OrderByDescending(item => item.score)
                    .Take(5);
                return top5Predicts.Select(item => new Movie { Id = 1, IdTmdb = item.movieId }).ToList();
            });
        }

        private class InputPredictModel
        {
            public int userId;
            public int movieId;
        }

        private class OutputPredictModel
        {
            public int movieId;
            public float score;
        }
    }
}
