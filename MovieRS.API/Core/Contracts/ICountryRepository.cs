using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface ICountryRepository
    {
        Task<Country?> GetByName(string name);
    }
}
