using MovieRS.API.Dtos.Review;
using TMDbLib.Objects.Reviews;

namespace MovieRS.API.Core.Contracts
{
    public interface IReviewRepository
    {
        Task<TMDbLib.Objects.General.SearchContainerWithId<UserReview>> GetReviews(Models.User user, int page = 1, int take = 0);
        Task<TMDbLib.Objects.Reviews.ReviewBaseExtension?> GetReviewById(int id);
        Task<List<Models.Review>> GetReviewsByIdMovie(int id);
        Task<bool> NewReview(Models.User user, Models.Movie movie, NewReviewDto review);
        Task<bool> UpdateReview(Models.User user, NewReviewDto newReview);
        Task<bool> DeleteReview(Models.User user, int id);
    }

    public class UserReview
    {
        public TMDbLib.Objects.Movies.Movie? Movie { get; set; }
        public string? Content { get; set; }
        public string? Id { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
