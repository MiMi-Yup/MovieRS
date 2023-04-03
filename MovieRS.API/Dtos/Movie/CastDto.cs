namespace MovieRS.API.Dtos.Movie
{
    public class CastDto
    {
        public PersonDto? Person { get; set; }
        public string? Character { get; set; }
        public string? CreditId { get; set; }
    }
}
