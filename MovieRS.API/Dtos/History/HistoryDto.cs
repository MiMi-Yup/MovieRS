using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Dtos.History
{
    public class HistoryDto
    {
        public DateTime? TimeStamp { get; set; }
        public MovieDto? Movie { get; set; }
    }
}
