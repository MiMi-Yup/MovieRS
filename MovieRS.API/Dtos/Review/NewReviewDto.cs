namespace MovieRS.API.Dtos.Review
{
    public class NewReviewDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public double? Rating { get; set; }
    }
}
