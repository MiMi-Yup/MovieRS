namespace TMDbLib.Objects.People
{
    public class MovieCreditsExtension
    {
        public int Id { get; set; }
        public IList<TMDbLib.Objects.People.MovieRoleExtension>? Cast { get; set; }
    }

    public static class MovieCreditExistExtension
    {
        public static MovieCreditsExtension Convert(this MovieCredits collection)
        {
            return new MovieCreditsExtension
            {
                Id = collection.Id,
                Cast = collection.Cast.Select(item => item.Convert()).ToList()
            };
        }
    }
}
