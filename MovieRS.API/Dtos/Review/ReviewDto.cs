using MovieRS.API.Dtos.User;
using TMDbLib.Objects.Reviews;

namespace MovieRS.API.Dtos.Review
{
    public class ReviewDto
    {
        public UserDto? AuthorDetails { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public string? Id { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
