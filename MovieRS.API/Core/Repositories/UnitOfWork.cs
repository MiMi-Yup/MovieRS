using AutoMapper;
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

        public UnitOfWork(MovieRsContext context, ILoggerFactory loggerFactory, IMapper mapper, IConfiguration configuration, ITMDb tmdb)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger("logs");
            _tmdb = tmdb;

            Country = new CountryRepository(_context, _logger, _mapper, _configuration);
            User = new UserRepository(_context, _configuration, Country, _logger, _mapper);
            Movie = new MovieRepository(_context, _logger, _mapper, _tmdb);
            CollectionMovie = new CollectionRepository(_context, _logger, _mapper, _tmdb, Movie);
            Person = new PersonRepository(_tmdb, Movie);

            /*Recommend = new RecommendRepository(context, _logger, _mapper, this, _configuration);*/
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
