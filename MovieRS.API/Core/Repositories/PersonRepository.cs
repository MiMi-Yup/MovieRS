using MovieRS.API.Core.Contracts;
using TMDbLib.Objects.People;

namespace MovieRS.API.Core.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ITMDb _tmdb;

        public PersonRepository(ITMDb tmdb)
        {
            _tmdb = tmdb;
        }

        public Task<TMDbLib.Objects.People.Person> GetPerson(int id)
        {
            _tmdb.ChangeLanguage("en");
            Task<TMDbLib.Objects.People.Person> task = _tmdb.Client.GetPersonAsync(id);
            _tmdb.ChangeLanguage("vi");
            return task;
        }

        public async Task<TMDbLib.Objects.People.MovieCreditsExtension?> GetMovieAct(IMovieRepository repository, int id, int take = 0)
        {
            var movie = await _tmdb.Client.GetPersonMovieCreditsAsync(id);
            return movie != null ? new TMDbLib.Objects.People.MovieCreditsExtension
            {
                Id = movie.Id,
                Cast = (await Task.WhenAll(movie.Cast.Take(take > 0 ? take : movie.Cast.Count).Select(async item =>
                {
                    TMDbLib.Objects.People.MovieRoleExtension role = item.Convert();
                    role.Movie = await repository.GetMovieBy3rd(item.Id);
                    return role;
                }))).ToList()
            } : null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>?> SearchPerson(string query, int page = 1, string? region = null)
        {
            var person = await _tmdb.Client.SearchPersonAsync(query, page, region: region);
            return person != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>
            {
                Page = person.Page,
                TotalPages = person.TotalPages,
                TotalResults = person.TotalResults,
                Results = (await Task.WhenAll(person.Results.Select(item => GetPerson(item.Id)))).ToList()
            } : null;
        }
    }
}
