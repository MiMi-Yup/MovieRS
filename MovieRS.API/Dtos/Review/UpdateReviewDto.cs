namespace MovieRS.API.Dtos.Review
{
    public class UpdateReviewDto
    {
        public string Id { get; set; } = null!;
        public string? Content { get; set; }
        public double? Rating { get; set; }
    }
}
