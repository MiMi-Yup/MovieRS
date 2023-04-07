using MovieRS.API.Dtos.Favourite;

namespace MovieRS.API.Core.Contracts
{
    public interface IFavouriteRepository
    {
        Task<IList<Models.Favourite>> GetFavourites(Models.User user);
        Task<Models.Favourite> NewFavourites(NewFavouriteDto newFavourite);
        Task<bool> DeleteFavourites(FavouriteDto deleteFavourite);
    }
}
