using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly IUserRepository _userRepository;
        public ReviewRepository(MovieRsContext context, ILogger logger, IMapper mapper, IUserRepository userRepository) : base(context, logger, mapper)
        {
            _userRepository = userRepository;
        }

        public Task<Review> GetReviews(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> NewReviews(NewReviewDto review)
        {
            if (review.Review.AuthorDetails == null)
                throw new ApiException("Not found user written review", System.Net.HttpStatusCode.BadRequest);
            User? user = await _userRepository.GetById(review.Review.AuthorDetails.Id.ToString());
            if (user == null)
                throw new ApiException("Not found user written review.Review", System.Net.HttpStatusCode.BadRequest);
            Review newReview = new Review { UserId = user.Id, Content = review.Review.Content, Rating = Convert.ToDecimal(review.Review.Rating), TimeStamp = DateTime.Now, MovieId = review.Id };
            if (await this.Add(newReview))
                await _context.SaveChangesAsync();
            return newReview;
        }
    }
}
