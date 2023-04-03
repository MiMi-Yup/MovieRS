using TMDbLib.Objects.People;

namespace TMDbLib.Objects.Movies
{
    public class CastExtension
    {
        public Person? Person { get; set; }
        public string? Character { get; set; }
        public string? CreditId { get; set; }
    }

    public static class CastExistExtension
    {
        public static CastExtension Convert(this Cast cast)
        {
            return new CastExtension
            {
                Character = cast.Character,
                CreditId = cast.CreditId
            };
        }
    }
}
