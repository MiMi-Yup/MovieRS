namespace MovieRS.API.Core.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ICountryRepository Country { get; }
        IMovieRepository Movie { get; }
        ICollectionRepository CollectionMovie { get; }
    }
}
