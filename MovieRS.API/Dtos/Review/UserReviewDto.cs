using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Dtos.Review
{
    public class UserReviewDto
    {
        public MovieDto? Movie { get; set; }
        public string? Content { get; set; }
        public string? Id { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
