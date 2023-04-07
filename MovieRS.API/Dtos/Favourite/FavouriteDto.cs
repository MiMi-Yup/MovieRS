using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Dtos.Favourite
{
    public class FavouriteDto
    {
        public int UserId { get; set; }
        public IList<MovieDto>? Movies { get; set; }
    }
}
