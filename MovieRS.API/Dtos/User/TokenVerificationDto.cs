namespace MovieRS.API.Dtos.User
{
    public class TokenVerificationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
