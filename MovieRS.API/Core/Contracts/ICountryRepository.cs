namespace MovieRS.API.Core.Contracts
{
    public interface ICountryRepository
    {
        Task<Models.Country> GetCountryById(int id);
        Task<Models.Country?> GetByName(string name);
        Task<Models.Country?> GetByCode(string code);
        Task<TMDbLib.Objects.General.SearchContainer<Models.Country>> GetAll(int page = 0, int take = 0);
    }
}
