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

        public async Task<TMDbLib.Objects.General.SearchContainer<Models.Country>> GetAll(int page = 1, int take = 0)
        {
            const int itemOfPage = 10;
            int count = dbSet.Count();
            return new TMDbLib.Objects.General.SearchContainer<Country>
            {
                Page = page > 1 ? page : 1,
                TotalPages = count / itemOfPage + (count % itemOfPage > 0 ? 1 : 0),
                TotalResults = count,
                Results = await dbSet.Skip(page > 1 ? (page - 1) * itemOfPage : 0).Take(take > 0 ? take : itemOfPage).ToListAsync()
            };
        }

        public Task<Country?> GetByCode(string code)
        {
            return this.Find(item => item.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Country?> GetByName(string name)
        {
            return await this.Find(item => item.Name == name).FirstOrDefaultAsync();
        }

        public Task<Country> GetCountryById(int id)
        {
            return this.FindById(id);
        }
    }
}
