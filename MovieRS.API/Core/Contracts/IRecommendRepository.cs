using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IRecommendRepository
    {
        Task<IList<TMDbLib.Objects.Movies.Movie>> GetUserMovieRecommendation(MovieRS.API.Models.User user, int takeMax = 0);
    }
}
