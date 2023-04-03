using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieRS.API.Dtos.Movie
{
    public class MovieDto
    {
        public SearchCollectionDto? BelongsToCollection { get; set; }
        public string? BackdropPath { get; set; }
        public long? Budget { get; set; }
        public List<Genre>? Genres { get; set; }
        public int Id { get; set; }
        public string? ImdbId { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Overview { get; set; }
        public string? PosterPath { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public long? Revenue { get; set; }
        public int? Runtime { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }
        public double? VoteAverage { get; set; }
        public int? VoteCount { get; set; }
    }
}
