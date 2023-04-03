using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace MovieRS.API.Core.Repositories
{
    public class MovieRepository : GenericRepository<MovieRS.API.Models.Movie>, IMovieRepository
    {
        private readonly ITMDb _tmdb;

        public MovieRepository(MovieRsContext context, ILogger logger, IMapper mapper, ITMDb tmdb) : base(context, logger, mapper)
        {
            _tmdb = tmdb;
        }

        public Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id)
        {
            _tmdb.ChangeLanguage(null);
            Task<TMDbLib.Objects.General.ImagesWithId> task = _tmdb.Client.GetMovieImagesAsync(id);
            _tmdb.ChangeLanguage("vi");
            return task;
        }

        public Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id)
        {
            return _tmdb.Client.GetMovieAsync(id);
        }

        public async Task<TMDbLib.Objects.Movies.CreditsExtension?> GetActors(IPersonRepository repository, int id)
        {
            TMDbLib.Objects.Movies.Credits credits = await _tmdb.Client.GetMovieCreditsAsync(id);
            if (credits != null)
            {
                return new TMDbLib.Objects.Movies.CreditsExtension
                {
                    Id = credits.Id,
                    Cast = (await Task.WhenAll(credits.Cast.Select(async item =>
                    {
                        TMDbLib.Objects.Movies.CastExtension cast = item.Convert();
                        cast.Person = await repository.GetPerson(item.Id);
                        return cast;
                    }))).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetNowPlaying(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieNowPlayingListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
                {
                    Dates = movie.Dates,
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = (take > 0 ? result.Take(take) : result).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetPopular(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMoviePopularListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = (take > 0 ? result.Take(take) : result).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetRecommendation(int id, int page = 1, int take = 0)
        {
            var recommendation = await _tmdb.Client.GetMovieRecommendationsAsync(id, page);
            if (recommendation != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(recommendation.Results.Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = recommendation.Page,
                    TotalPages = recommendation.TotalPages,
                    TotalResults = recommendation.TotalResults,
                    Results = (take > 0 ? result.Take(take) : result).ToList()
                };
            }
            return null;
        }

        public Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id, int page = 1)
        {
            return _tmdb.Client.GetMovieReviewsAsync(id, page: page < 1 ? 1 : page);
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetTopRated(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieTopRatedListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = (take > 0 ? result.Take(take) : result).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetUpComming(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieUpcomingListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
                {
                    Dates = movie.Dates,
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = (take > 0 ? result.Take(take) : result).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> SearchMovie(string query, int year = 0, string? region = null, int primaryReleaseYear = 0, int page = 1)
        {
            var movie = await _tmdb.Client.SearchMovieAsync(query, page: page < 1 ? 1 : page, year: year, region: region, primaryReleaseYear: primaryReleaseYear);
            return movie != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
            {
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }
    }
}
