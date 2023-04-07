using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Favourite;
using MovieRS.API.Error;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Repositories
{
    public class FavouriteRepository : GenericRepository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(MovieRsContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public async Task<bool> DeleteFavourites(FavouriteDto deleteFavourite)
        {
            IEnumerable<Favourite?>? find = deleteFavourite.Movies?
                .Select(item => dbSet.FirstOrDefault(_item => _item.UserId == deleteFavourite.UserId && _item.MovieId == item.Id))
                .Where(item => item != null);

            if (find != null && find.Any() && await this.Delete(find))
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IList<Favourite>> GetFavourites(User user)
        {
            return await this.Find(item => item.UserId == user.Id, /*item => item.OrderByDescending(_item => _item.Timestamp),*/ trackChanges: false).ToListAsync();
        }

        public async Task<Favourite> NewFavourites(NewFavouriteDto newFavourite)
        {
            try
            {
                Favourite newItem = new Favourite { UserId = newFavourite.UserId, MovieId = newFavourite.MovieId };
                if (await this.Add(newItem))
                {
                    await _context.SaveChangesAsync();
                    return newItem;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
