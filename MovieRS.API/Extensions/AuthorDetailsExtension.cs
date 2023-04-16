namespace TMDbLib.Objects.Reviews
{
    public class AuthorDetailsExtension
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? AvatarPath { get; set; }
        public string Email { get; set; } = null!;
        public MovieRS.API.Models.Country? Country { get; set; }
    }

    public static class AuthorDetailsExistExtension
    {
        public static AuthorDetailsExtension Convert(this AuthorDetails auth)
        {
            return new AuthorDetailsExtension
            {
                Id = 0,
                AvatarPath = auth.AvatarPath,
                Username = string.IsNullOrEmpty(auth.Username) ? auth.Name : auth.Username,
            };
        }
    }
}
