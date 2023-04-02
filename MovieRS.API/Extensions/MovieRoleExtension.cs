namespace TMDbLib.Objects.People
{
    public class MovieRoleExtension
    {
        public TMDbLib.Objects.Movies.Movie? Movie { get;set; }
        public string? CreditId { get; set; }
        public string? Character { get; set; }
    }

    public static class MovieRoleExistExtension
    {
        public static MovieRoleExtension Convert(this MovieRole role)
        {
            return new MovieRoleExtension
            {
                CreditId = role.CreditId,
                Character = role.Character
            };
        }
    }
}
