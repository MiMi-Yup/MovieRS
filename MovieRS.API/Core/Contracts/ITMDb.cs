using TMDbLib.Client;

namespace MovieRS.API.Core.Contracts
{
    public interface ITMDb
    {
        TMDbClient Client { get; }
        void ChangeToVN();
        void ChangeToEN();
    }
}
