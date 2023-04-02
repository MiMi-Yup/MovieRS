using MovieRS.API.Core.Contracts;
using TMDbLib.Objects.People;

namespace MovieRS.API.Core.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ITMDb _tmdb;
        private readonly IMovieRepository _movieRepository;

        public PersonRepository(ITMDb tmdb, IMovieRepository movieRepository)
        {
            _tmdb = tmdb;
            _movieRepository = movieRepository;
        }

        public Task<TMDbLib.Objects.People.Person> GetPerson(int id)
        {
            _tmdb.ChangeLanguage("en");
            Task<TMDbLib.Objects.People.Person> task = _tmdb.Client.GetPersonAsync(id);
            _tmdb.ChangeLanguage("vi");
            return task;
        }

        public async Task<TMDbLib.Objects.People.MovieCreditExtension?> GetMovieAct(int id)
        {
            var movie = await _tmdb.Client.GetPersonMovieCreditsAsync(id);
            return movie != null ? new TMDbLib.Objects.People.MovieCreditExtension
            {
                Id = movie.Id,
                Cast = (await Task.WhenAll(movie.Cast.Select(async item =>
                {
                    TMDbLib.Objects.People.MovieRoleExtension role = item.Convert();
                    role.Movie = await _movieRepository.GetMovie(item.Id);
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
