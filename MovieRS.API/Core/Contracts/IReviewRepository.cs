using MovieRS.API.Dtos.Movie;

namespace MovieRS.API.Core.Contracts
{
    public interface IReviewRepository
    {
        Task<Models.Review> GetReviews(int id);
        Task<Models.Review> NewReviews(NewReviewDto review);
    }
}
