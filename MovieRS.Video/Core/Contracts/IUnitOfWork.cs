namespace MovieRS.Video.Core.Contracts
{
    public interface IUnitOfWork
    {
        IVideoRepository Video { get; }
    }
}
