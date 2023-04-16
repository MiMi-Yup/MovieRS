namespace MovieRS.API.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public CountryDto? Country { get; set; }
    }
}
