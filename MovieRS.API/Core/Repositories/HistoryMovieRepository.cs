using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.History;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class HistoryMovieRepository : GenericRepository<History>, IHistoryMovieRepository
    {
        private const int MAX_ITEM_OF_PAGE = 20;
        private readonly IMovieRepository _movieRepository;
        public HistoryMovieRepository(MovieRsContext context, ILogger logger, IMapper mapper, IMovieRepository movieRepository) : base(context, logger, mapper)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> ClearHistory(User user)
        {
            IEnumerable<History> histories = dbSet.Where(item => item.UserId == user.Id);
            try
            {
                if (await this.Delete(histories))
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<HistoryMovie>> GetHistories(User user, int take = 0, int page = 1)
        {
            IEnumerable<History> histories = dbSet.Where(item => item.UserId == user.Id).AsNoTracking();
            IEnumerable<History> rangeHistories = histories.OrderByDescending(item => item.TimeStamp).Skip((page > 1 ? page : 1) * MAX_ITEM_OF_PAGE).Take(take > 0 ? take : MAX_ITEM_OF_PAGE);
            HistoryMovie[] movies = await Task.WhenAll(rangeHistories
                .Where(item => item.Movie.IdTmdb != null)
                .Select(async item => new HistoryMovie { UserId = item.UserId, TimeStamp = item.TimeStamp, Movie = await _movieRepository.GetMovie(item.Movie.IdTmdb.Value) }));
            return new TMDbLib.Objects.General.SearchContainer<HistoryMovie>
            {
                Page = page > 1 ? page : 1,
                TotalPages = (int)Math.Ceiling(histories.Count() * 1.0 / MAX_ITEM_OF_PAGE),
                TotalResults = histories.Count(),
                Results = movies.ToList()
            };
        }

        public async Task<bool> AddHistory(User user, NewHistoryDto newHistory)
        {
            TMDbLib.Objects.Movies.Movie movie = await _movieRepository.GetMovie(newHistory.MovieId);
            if (movie == null) return false;
            if (await _movieRepository.NewVideo(movie))
            {
                Models.History history = new History { MovieId = movie.Id, UserId = user.Id, TimeStamp = DateTime.Now };
                if (await this.Add(history))
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
