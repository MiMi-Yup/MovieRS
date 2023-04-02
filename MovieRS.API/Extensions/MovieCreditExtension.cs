namespace TMDbLib.Objects.People
{
    public class MovieCreditExtension
    {
        public int Id { get; set; }
        public IList<TMDbLib.Objects.People.MovieRoleExtension>? Cast { get; set; }
    }

    public static class MovieCreditExistExtension
    {
        public static MovieCreditExtension Convert(this MovieCredits collection)
        {
            return new MovieCreditExtension
            {
                Id = collection.Id,
                Cast = collection.Cast.Select(item => item.Convert()).ToList()
            };
        }
    }
}
