namespace MovieRS.API.Core.Contracts
{
    public interface IFavouriteRepository
    {
        Task<bool> GetFavourites(string idUser);
        Task<bool> NewFavourites(string idUser);
        Task<bool> DeleteFavourites(string idUser);
    }
}
