using MovieRS.API.Dtos.History;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IHistoryMovieRepository
    {
        Task<TMDbLib.Objects.General.SearchContainer<HistoryMovie>> GetHistories(Models.User user, int take = 0, int page = 1);
        Task<bool> ClearHistory(Models.User user);
        Task<bool> AddHistory(User user, NewHistoryDto newHistory);
    }

    public class HistoryMovie
    {
        public int UserId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public TMDbLib.Objects.Movies.Movie Movie { get; set; } = null!;
    }
}
