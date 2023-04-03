namespace MovieRS.API.Core.Contracts
{
    public interface IPersonRepository
    {
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>?> SearchPerson(string query, int page = 1, string? region = null);
        Task<TMDbLib.Objects.People.Person> GetPerson(int id);
        Task<TMDbLib.Objects.People.MovieCreditsExtension?> GetMovieAct(IMovieRepository repository, int id);
    }
}
