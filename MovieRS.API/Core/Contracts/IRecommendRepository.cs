using MovieRS.API.Core.Repositories;

namespace MovieRS.API.Core.Contracts
{
    public interface IRecommendRepository
    {
        Task<ResultRecommendation> GetUserMovieRecommendation(Models.User user, int takeMax = 0);
    }

    public class Recommendation
    {
        public float Score { get; set; }
        public TMDbLib.Objects.Movies.Movie? Movie { get; set; }
    }

    public class ResultRecommendation
    {
        public int UserId { get; set; }
        public IList<Recommendation>? Results { get; set; }
    }
}
