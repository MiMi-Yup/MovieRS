namespace MovieRS.API.Dtos.Recommendation
{
    public class ResultRecommendationDto
    {
        public int UserId { get; set; }
        public IList<RecommendationDto>? Results { get; set; }
    }
}
