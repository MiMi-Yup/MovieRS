using Newtonsoft.Json;

namespace TMDbLib.Objects.Reviews
{
    public class ReviewBaseExtension
    {
        public string? Author { get; set; }
        public AuthorDetailsExtension? AuthorDetails { get; set; }
        public string? Content { get; set; }
        public string? Id { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public static class ReviewBaseExistExtension
    {
        public static ReviewBaseExtension Convert(this ReviewBase review)
        {
            return new ReviewBaseExtension
            {
                Author = review.Author,
                Content = review.Content,
                Id = review.Id,
                AuthorDetails = review.AuthorDetails.Convert(),
                Rating = System.Convert.ToDouble(review.AuthorDetails.Rating),
                CreatedAt = null
            };
        }
    }
}
