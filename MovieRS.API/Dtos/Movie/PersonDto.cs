using TMDbLib.Objects.People;

namespace MovieRS.API.Dtos.Movie
{
    public class PersonDto
    {
        public string? Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public PersonGender Gender { get; set; }
        public string? Homepage { get; set; }
        public int Id { get; set; }
        public string? ImdbId { get; set; }
        public string? Name { get; set; }
        public string? PlaceOfBirth { get; set; }
        public double? Popularity { get; set; }
        public string? ProfilePath { get; set; }
    }
}
