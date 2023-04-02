namespace MovieRS.API.Core.Contracts
{
    public interface IMovieRepository
    {
        Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id);
        Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetRecommendation(int id);
        Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id);
        Task<TMDbLib.Objects.Collections.Collection> GetCollection(int id);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetPopular();
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Search.SearchMovie>> GetNowPlaying();
        Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Search.SearchMovie>> GetUpComming();
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> GetTopRated();

        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchCollection>> SearchCollection(string query);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchMovie>> SearchMovie(string query);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchPerson>> SearchPeople(string query);
    }
}
