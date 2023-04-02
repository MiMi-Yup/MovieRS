namespace TMDbLib.Objects.Collections
{
    public class CollectionExtension
    {
        public string? BackdropPath { get; set; }
        public int? Id { get; set; }
        public TMDbLib.Objects.General.Images? Images { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public IList<TMDbLib.Objects.Movies.Movie>? Parts { get; set; }
        public string? PosterPath { get; set; }
    }

    public static class CollectionExistExtension
    {
        public static CollectionExtension Convert(this Collection collection)
        {
            return new CollectionExtension
            {
                BackdropPath = collection.BackdropPath,
                Id = collection.Id,
                Images = collection.Images,
                Name = collection.Name,
                Overview = collection.Overview,
                PosterPath = collection.PosterPath,
            };
        }
    }
}
