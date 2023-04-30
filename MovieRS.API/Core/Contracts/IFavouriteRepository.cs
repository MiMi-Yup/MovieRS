using MovieRS.API.Dtos.Favourite;
using TMDbLib.Objects.General;

namespace MovieRS.API.Core.Contracts
{
    public interface IFavouriteRepository
    {
        Task<SearchContainerWithId<TMDbLib.Objects.Movies.Movie>> GetFavourites(Models.User user, int page = 1, int take = 0);
        Task<int> NewFavourites(NewFavouriteDto newFavourite);
        Task<bool> DeleteFavourites(Models.User user, int idTmdb);
    }
}
