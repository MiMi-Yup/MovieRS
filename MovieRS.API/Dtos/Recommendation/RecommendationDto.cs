using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Dtos.Recommendation
{
    public class RecommendationDto
    {
        public float Score { get; set; }
        public MovieDto? Movie { get; set; }
    }
}
