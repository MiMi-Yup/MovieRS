namespace MovieRS.API.Dtos.Movie
{
    public class MovieCreditsDto
    {
        public int Id { get; set; }
        public IList<MovieRoleActDto>? Cast { get; set; }
    }
}
