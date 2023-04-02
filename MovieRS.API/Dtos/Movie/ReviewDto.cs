using MovieRS.API.Dtos.User;

namespace MovieRS.API.Dtos.Movie
{
    public class ReviewDto
    {
        public string? Author { get; set; }
        public UserDto? AuthorDetails { get; set; }
        public string? Content { get; set; }
        public string? Id { get; set; }
    }
}
