using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;
using TMDbLib.Client;

namespace MovieRS.API.Core.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly ITMDb _tmdb;

        public MovieRepository(MovieRsContext context, ITMDb tmdb, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
            _tmdb = tmdb;
        }

        public Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id)
        {
            return _tmdb.Client.GetMovieImagesAsync(id);
        }

        public Task<TMDbLib.Objects.Collections.Collection> GetCollection(int id)
        {
            return _tmdb.Client.GetCollectionAsync(id);
        }

        public Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id)
        {
            return _tmdb.Client.GetMovieAsync(id);
        }

        public Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Search.SearchMovie>> GetNowPlaying()
        {
            return _tmdb.Client.GetMovieNowPlayingListAsync();
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetPopular()
        {
            return _tmdb.Client.GetMoviePopularListAsync();
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetRecommendation(int id)
        {
            return _tmdb.Client.GetMovieRecommendationsAsync(id);
        }

        public Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id)
        {
            return _tmdb.Client.GetMovieReviewsAsync(id);
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetTopRated()
        {
            return _tmdb.Client.GetMovieTopRatedListAsync();
        }

        public Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Search.SearchMovie>> GetUpComming()
        {
            return _tmdb.Client.GetMovieUpcomingListAsync();
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchCollection>> SearchCollection(string query)
        {
            return _tmdb.Client.SearchCollectionAsync(query);
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> SearchMovie(string query)
        {
            return _tmdb.Client.SearchMovieAsync(query);
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchPerson>> SearchPeople(string query)
        {
            return _tmdb.Client.SearchPersonAsync(query);
        }
    }
}
