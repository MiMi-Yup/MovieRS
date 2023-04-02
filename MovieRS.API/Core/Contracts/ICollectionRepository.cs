namespace MovieRS.API.Core.Contracts
{
    public interface ICollectionRepository
    {
        Task<TMDbLib.Objects.Collections.CollectionExtension> GetCollection(int id);
        Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchCollection>> SearchCollection(string query);
    }
}
