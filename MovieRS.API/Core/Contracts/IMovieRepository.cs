using MovieRS.API.Dtos.Review;

namespace MovieRS.API.Core.Contracts
{
    public interface IMovieRepository
    {
        Task<TMDbLib.Objects.Movies.Movie> GetMovieBy3rd(int id);
        Task<Models.Movie> GetMovieById(int id);
        Task<Models.Movie?> GetMovieByIdTmdb(int id);

        Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetRecommendation(int id, int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBaseExtension>> GetReview(int id, int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetPopular(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetNowPlaying(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetUpComming(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetTopRated(int page = 1, int take = 0);
        Task<TMDbLib.Objects.Movies.CreditsExtension?> GetActors(IPersonRepository repository, int id, int take = 0);
        Task<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>> GetVideos(int id);
        Task<bool> NewReview(Models.User user, NewReviewDto newReview);
        Task<IEnumerable<MovieRS.API.Models.Movie>> GetLocalVideos();

        Task<bool> NewVideo(int idTmdb);

        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> SearchMovie(string query, int year = 0, string? region = null, int primaryReleaseYear = 0, int page = 1);
    }
}
