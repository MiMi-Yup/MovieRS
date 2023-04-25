namespace MovieRS.API.Core.Contracts
{
    public interface IVideoAPI
    {
        Task<bool> UpdateDomain(string domain);
        Task<string> GetLink(int id);
    }
}
