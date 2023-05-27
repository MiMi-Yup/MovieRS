namespace MovieRS.API.Dtos.Movie
{
    public class MovieWithSimilarDto
    {
        public MovieDto? Movie { get; set; }
        public IList<MovieDto>? SimilarMovies { get; set; }
    }
}
