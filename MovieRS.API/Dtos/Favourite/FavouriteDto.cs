using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Dtos.Favourite
{
    public class FavouriteDto
    {
        public DateTime? TimeStamp { get; set; }
        public MovieDto? Movie { get; set; }
    }
}
