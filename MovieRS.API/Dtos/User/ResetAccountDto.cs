namespace MovieRS.API.Dtos.User
{
    public class ResetAccountDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
