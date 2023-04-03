namespace TMDbLib.Objects.Movies
{
    public class CreditsExtension
    {
        public int Id { get; set; }
        public IList<CastExtension>? Cast { get; set; }
    }

    public static class CreditsExistExtension
    {
        public static CreditsExtension Convert(this Credits credits)
        {
            return new CreditsExtension
            {
                Id = credits.Id,
                Cast = credits.Cast.Select(item => item.Convert()).ToList(),
            };
        }
    }
}
