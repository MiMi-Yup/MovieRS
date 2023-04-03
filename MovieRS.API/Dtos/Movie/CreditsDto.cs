namespace MovieRS.API.Dtos.Movie
{
    public class CreditsDto
    {
        public int Id { get; set; }
        public IList<CastDto>? Cast { get; set; }
    }
}
