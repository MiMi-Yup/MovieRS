namespace MovieRS.API.Core.Contracts
{
    public interface IImageRepository
    {
        Task<byte[]> GetImage(string path, string size = "original", bool useSsl = true);
    }
}
