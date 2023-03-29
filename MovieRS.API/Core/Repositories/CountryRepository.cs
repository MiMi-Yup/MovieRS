using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Core.Repositories;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly IConfiguration _configuration;
        public CountryRepository(MovieRsContext context, ILogger logger, IMapper mapper, IConfiguration configuration) : base(context, logger, mapper)
        {
            _configuration = configuration;
        }

        public async Task<Country?> GetByName(string name)
        {
            return await this.Find(item => item.Name == name).FirstOrDefaultAsync();
        }
    }
}
