using AutoMapper;
using Microsoft.ML;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Core.Repositories;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MovieRsContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ITMDb _tmdb;

        public IUserRepository User { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IRecommendRepository Recommend { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public ICollectionRepository CollectionMovie { get; private set; }
        public IPersonRepository Person { get; private set; }
        public IImageRepository Image { get; private set; }
        public IReviewRepository Review { get; private set; }

        public UnitOfWork(MovieRsContext context, ILoggerFactory loggerFactory, IMapper mapper, IConfiguration configuration, ITMDb tmdb, MLContext mlContext)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger("logs");
            _tmdb = tmdb;

            Country = new CountryRepository(_context, _logger, _mapper, _configuration);
            User = new UserRepository(_context, _configuration, Country, _logger, _mapper);
            Review = new ReviewRepository(_context, _logger, _mapper, User);
            Movie = new MovieRepository(_context, _logger, _mapper, _tmdb, Review);
            Person = new PersonRepository(_tmdb);
            CollectionMovie = new CollectionRepository(_context, _logger, _mapper, _tmdb, Movie);
            Image = new ImageRepository(_tmdb);

            Recommend = new RecommendRepository(context, _logger, _mapper, _configuration, Movie, mlContext);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
