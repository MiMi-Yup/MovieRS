using MovieRS.Video.Core.Contracts;
using MovieRS.Video.Models;

namespace MovieRS.Video.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly DatabaseContext _context;

        public IVideoRepository Video { get; private set; }

        public UnitOfWork(IConfiguration configuration, ILoggerFactory loggerFactory, DatabaseContext context)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
            _context = context;

            Video = new VideoRepository(_context);
        }
    }
}
