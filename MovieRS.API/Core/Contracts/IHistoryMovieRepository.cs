using MovieRS.API.Dtos.History;

namespace MovieRS.API.Core.Contracts
{
    public interface IHistoryMovieRepository
    {
        Task<TMDbLib.Objects.General.SearchContainerWithId<HistoryMovie>> GetHistories(Models.User user, int page = 1, int take = 0);
        Task<bool> ClearHistory(Models.User user);
        Task<bool> AddHistory(Models.User user, AddHistoryDto addHistory);
    }

    public class HistoryMovie
    {
        public DateTime? TimeStamp { get; set; }
        public TMDbLib.Objects.Movies.Movie? Movie { get; set; }
    }
}
