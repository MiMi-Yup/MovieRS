using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Review;
using MovieRS.API.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Reviews;

namespace MovieRS.API.Core.Repositories
{
    public class ReviewRepository : GenericRepository<Models.Review>, IReviewRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewRepository(MovieRsContext context, ILogger logger, IMapper mapper, IUnitOfWork unitOfWork) : base(context, logger, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteReview(User user, int id)
        {
            Models.Review? review = await dbSet.FindAsync(id);
            if (review == null || review.UserId != user.Id)
                return false;
            dbSet.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<ReviewBaseExtension?> GetReviewById(int id)
        {
            Models.Review? review = dbSet.Include(r => r.User).FirstOrDefault(item => item.Id == id);
            return Task.FromResult(review == null ? null : new ReviewBaseExtension
            {
                Author = review.User.Username,
                Content = review.Content,
                Id = review.Id.ToString(),
                Rating = System.Convert.ToDouble(review.Rating),
                CreatedAt = review.TimeStamp,
                AuthorDetails = new AuthorDetailsExtension
                {
                    Id = review.User.Id,
                    Username = review.User.Username,
                    Email = review.User.Email,
                    Country = review.User.Country,
                    AvatarPath = null,
                }
            });
        }

        public async Task<SearchContainerWithId<UserReview>> GetReviews(User user, int page = 1, int take = 0)
        {
            const int MAX_ITEM_OF_PAGE = 10;
            IQueryable<Models.Review> query = dbSet.Include(u => u.User).Include(m => m.Movie)
                .Where(item => item.UserId == user.Id && !string.IsNullOrWhiteSpace(item.Content))
                .AsNoTracking();
            int count = query.Count();
            page = page > 1 ? page : 1;
            return new TMDbLib.Objects.General.SearchContainerWithId<UserReview>
            {
                Id = user.Id,
                Page = page,
                TotalPages = (int)Math.Ceiling(count * 1.0 / MAX_ITEM_OF_PAGE),
                TotalResults = count,
                Results = (await Task.WhenAll(query.Skip((page - 1) * MAX_ITEM_OF_PAGE).Take(take > 0 ? take : MAX_ITEM_OF_PAGE).ToList().Select(async item => new UserReview
                {
                    Content = item.Content,
                    Id = item.Id.ToString(),
                    Rating = System.Convert.ToDouble(item.Rating),
                    CreatedAt = item.TimeStamp,
                    Movie = await _unitOfWork.Movie.GetMovieBy3rd(item.Movie.IdTmdb.Value)
                }))).ToList()
            };
        }

        public Task<List<Models.Review>> GetReviewsByIdMovie(int id)
        {
            return dbSet.Include(r => r.User).Where(item => item.Movie.IdTmdb == id && !string.IsNullOrWhiteSpace(item.Content)).ToListAsync();
        }

        public async Task<bool> NewReview(User user, Movie movie, NewReviewDto review)
        {
            Models.Review newReview = new Models.Review
            {
                UserId = user.Id,
                Content = review.Content,
                Rating = Convert.ToDecimal(review.Rating),
                TimeStamp = DateTime.Now,
                MovieId = movie.Id
            };
            if (await this.Add(newReview))
                await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReview(User user, UpdateReviewDto updateReview)
        {
            if (!int.TryParse(updateReview.Id, out int IdReview))
                return false;
            Models.Review? review = await dbSet.FindAsync(IdReview);
            if (review == null || review.UserId != user.Id)
                return false;
            review.Content = updateReview.Content;
            review.Rating = updateReview.Rating == null
                ? review.Rating
                : Convert.ToDecimal(updateReview.Rating);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
