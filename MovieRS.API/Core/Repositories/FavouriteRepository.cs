using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Favourite;
using MovieRS.API.Error;
using TMDbLib.Objects.General;

namespace MovieRS.API.Core.Repositories
{
    public class FavouriteRepository : GenericRepository<Models.Favourite>, IFavouriteRepository
    {
        private readonly IMovieRepository _movieRepository;
        public FavouriteRepository(Models.MovieRsContext context, ILogger logger, IMapper mapper, IMovieRepository movieRepository) : base(context, logger, mapper)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> DeleteFavourites(Models.User user, int idTmdb)
        {
            Models.Movie? find = await _movieRepository.GetMovieByIdTmdb(idTmdb);
            if (find == null) return false;

            Models.Favourite? favourite = await dbSet.SingleOrDefaultAsync(item => item.UserId == user.Id && item.MovieId == find.Id);

            if (favourite != null)
            {
                this.Delete(favourite);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<SearchContainerWithId<UserFavourite>> GetFavourites(Models.User user, int page = 1, int take = 0)
        {
            const int itemOfPage = 10;
            List<Models.Favourite> favourites = await dbSet.Include(m => m.Movie)
                .Where(item => item.UserId == user.Id && item.Movie.IdTmdb != null)
                .OrderByDescending(item => item.TimeStamp)
                .Skip((page > 1 ? page - 1 : 0) * itemOfPage)
                .Take(take > 0 ? take : itemOfPage).AsNoTracking().ToListAsync();
            return new SearchContainerWithId<UserFavourite>
            {
                Id = user.Id,
                Page = page,
                TotalPages = (int)Math.Ceiling(favourites.Count() * 1.0 / itemOfPage),
                TotalResults = favourites.Count,
                Results = (await Task.WhenAll(favourites.Select(async item => new UserFavourite
                {
                    TimeStamp = item.TimeStamp,
                    Movie = await _movieRepository.GetMovieBy3rd(item.Movie.IdTmdb!.Value)
                }))).ToList()
            };
        }

        public async Task<int> NewFavourites(NewFavouriteDto newFavourite)
        {
            try
            {
                Models.Movie? find = await _movieRepository.GetMovieByIdTmdb(newFavourite.MovieId);
                if (find == null)
                {
                    await _movieRepository.NewVideo(newFavourite.MovieId);
                    find = await _movieRepository.GetMovieByIdTmdb(newFavourite.MovieId);
                }
                Models.Favourite? favFind = await dbSet.SingleOrDefaultAsync(item => item.UserId == newFavourite.UserId && item.MovieId == find.Id);
                if (favFind != null) return 1;
                if (await this.Add(new Models.Favourite
                {
                    UserId = newFavourite.UserId,
                    MovieId = find.Id,
                    TimeStamp = DateTime.Now
                }))
                {
                    await _context.SaveChangesAsync();
                    return 0;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
