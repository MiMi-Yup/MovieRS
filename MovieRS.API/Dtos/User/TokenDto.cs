namespace MovieRS.API.Dtos.User
{
    public class TokenDto
    {
        public string Value { get; set; } = null!;
        public DateTime ExpiredAt;
        public RegisterUserDto User = null!;
    }
}
