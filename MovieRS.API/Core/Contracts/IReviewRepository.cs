using MovieRS.API.Dtos.Review;

namespace MovieRS.API.Core.Contracts
{
    public interface IReviewRepository
    {
        Task<Models.Review> GetReviewsById(int id);
        Task<List<Models.Review>> GetReviewsByIdMovie(int id);
        Task<bool> NewReviews(Models.User user, Models.Movie movie, NewReviewDto review);
        Task<bool> DeleteReviews(int id);
    }
}
