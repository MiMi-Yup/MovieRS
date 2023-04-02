using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;

namespace MovieRS.API.Core.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
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

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetNowPlaying()
        {
            var movie = await _tmdb.Client.GetMovieNowPlayingListAsync();
            return movie != null ? new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
            {
                Dates = movie.Dates,
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetPopular()
        {
            var movie = await _tmdb.Client.GetMoviePopularListAsync();
            return movie != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
            {
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetRecommendation(int id, int page = 1)
        {
            var recommendation = await _tmdb.Client.GetMovieRecommendationsAsync(id, page);
            return recommendation != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
            {
                Page = recommendation.Page,
                TotalPages = recommendation.TotalPages,
                TotalResults = recommendation.TotalResults,
                Results = (await Task.WhenAll(recommendation.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id)
        {
            return _tmdb.Client.GetMovieReviewsAsync(id);
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetTopRated()
        {
            var movie = await _tmdb.Client.GetMovieTopRatedListAsync();
            return movie != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
            {
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetUpComming()
        {
            var movie = await _tmdb.Client.GetMovieUpcomingListAsync();
            return movie != null ? new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
            {
                Dates = movie.Dates,
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> SearchMovie(string query, int year = 0, string? region = null, int primaryReleaseYear = 0, int page = 1)
        {
            var movie = await _tmdb.Client.SearchMovieAsync(query, page: page, year: year, region: region, primaryReleaseYear: primaryReleaseYear);
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
