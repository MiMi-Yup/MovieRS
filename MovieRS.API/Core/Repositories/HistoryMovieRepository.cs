using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.History;
using MovieRS.API.Error;

namespace MovieRS.API.Core.Repositories
{
    public class HistoryMovieRepository : GenericRepository<Models.History>, IHistoryMovieRepository
    {
        private const int MAX_ITEM_OF_PAGE = 20;
        private readonly IMovieRepository _movieRepository;
        public HistoryMovieRepository(Models.MovieRsContext context, ILogger logger, IMapper mapper, IMovieRepository movieRepository) : base(context, logger, mapper)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> ClearHistory(Models.User user)
        {
            IEnumerable<Models.History> histories = dbSet.Where(item => item.UserId == user.Id);
            try
            {
                dbSet.RemoveRange(histories);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithId<HistoryMovie>> GetHistories(Models.User user, int page = 1, int take = 0)
        {
            IEnumerable<Models.History> histories = dbSet.Include(h => h.Movie).Where(item => item.UserId == user.Id).AsNoTracking();
            IEnumerable<Models.History> rangeHistories = histories
                .OrderByDescending(item => item.TimeStamp)
                .Skip((page > 1 ? page - 1 : 0) * MAX_ITEM_OF_PAGE)
                .Take(take > 0 ? take : MAX_ITEM_OF_PAGE);
            HistoryMovie[] movies = await Task.WhenAll(rangeHistories
                .Where(item => item.Movie.IdTmdb != null)
                .Select(async item => new HistoryMovie { TimeStamp = item.TimeStamp, Movie = await _movieRepository.GetMovieBy3rd(item.Movie.IdTmdb!.Value) }));
            return new TMDbLib.Objects.General.SearchContainerWithId<HistoryMovie>
            {
                Id = user.Id,
                Page = page > 1 ? page : 1,
                TotalPages = (int)Math.Ceiling(histories.Count() * 1.0 / MAX_ITEM_OF_PAGE),
                TotalResults = histories.Count(),
                Results = movies.ToList()
            };
        }

        public async Task<bool> AddHistory(Models.User user, AddHistoryDto addHistory)
        {
            Models.Movie? movie = await _movieRepository.GetMovieByIdTmdb(addHistory.IdMovie);
            if (movie == null && await _movieRepository.NewVideo(addHistory.IdMovie))
                movie = await _movieRepository.GetMovieByIdTmdb(addHistory.IdMovie);
            if (movie != null)
            {
                Models.History? history = await dbSet.SingleOrDefaultAsync(item => item.UserId == user.Id && item.MovieId == movie.Id);
                if (history != null)
                {
                    history.TimeStamp = DateTime.Now;
                    history.Rating = addHistory.Rating == null 
                        ? history.Rating 
                        : Convert.ToDecimal(addHistory.Rating);
                }
                else
                {
                    history = new Models.History
                    {
                        MovieId = movie.Id,
                        UserId = user.Id,
                        TimeStamp = DateTime.Now,
                        Rating = addHistory.Rating == null
                            ? null
                            : Convert.ToDecimal(addHistory.Rating)
                    };
                    await this.Add(history);
                }
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
