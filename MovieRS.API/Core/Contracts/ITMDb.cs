using TMDbLib.Client;

namespace MovieRS.API.Core.Contracts
{
    public interface ITMDb
    {
        TMDbClient Client { get; }
        void ChangeLanguage(string? lang);
    }
}
