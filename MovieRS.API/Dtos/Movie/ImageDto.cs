namespace MovieRS.API.Dtos.Movie
{
    public class ImageDto
    {
        public int Id { get; set; }
        public IList<ImageDataDto>? Posters { get; set; }
        public IList<ImageDataDto>? Backdrops { get; set; }
    }
}
