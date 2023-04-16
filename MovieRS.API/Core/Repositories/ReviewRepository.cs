using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Review;
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

        public Task<bool> DeleteReviews(int id)
        {
            return this.Delete(id);
        }

        public Task<Review> GetReviewsById(int id)
        {
            return this.FindById(id);
        }

        public Task<List<Review>> GetReviewsByIdMovie(int id)
        {
            return dbSet.Include(r => r.User).Where(item => item.Movie.IdTmdb == id).ToListAsync();
        }

        public async Task<bool> NewReviews(User user, Movie movie, NewReviewDto review)
        {
            if (user == null || review == null)
                throw new ApiException("Null value exception", System.Net.HttpStatusCode.BadRequest);
            Review newReview = new Review { UserId = user.Id, Content = review.Content, Rating = Convert.ToDecimal(review.Rating), TimeStamp = DateTime.Now, MovieId = movie.Id };
            if (await this.Add(newReview))
                await _context.SaveChangesAsync();
            return true;
        }
    }
}
