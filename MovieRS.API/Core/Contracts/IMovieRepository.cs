namespace MovieRS.API.Core.Contracts
{
    public interface IMovieRepository
    {
        Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id);
        Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetRecommendation(int id, int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id, int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetPopular(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetNowPlaying(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetUpComming(int page = 1, int take = 0);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetTopRated(int page = 1, int take = 0);
        Task<TMDbLib.Objects.Movies.CreditsExtension?> GetActors(IPersonRepository repository, int id, int take = 0);
        Task<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>> GetVideos(int id);

        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> SearchMovie(string query, int year = 0, string? region = null, int primaryReleaseYear = 0, int page = 1);
    }
}
