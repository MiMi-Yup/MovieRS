using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IRecommendRepository
    {
        Task<IList<Movie>> GetRecommends();
    }
}
