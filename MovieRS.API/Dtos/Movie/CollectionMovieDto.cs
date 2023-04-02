namespace MovieRS.API.Dtos.Movie
{
    public class CollectionMovieDto
    {
        public string? BackdropPath { get; set; }
        public int Id { get; set; }
        public ImageDto? Images { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public IList<MovieDto>? Parts { get; set; }
        public string? PosterPath { get; set; }
    }
}
