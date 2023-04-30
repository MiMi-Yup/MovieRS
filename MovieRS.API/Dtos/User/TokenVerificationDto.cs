namespace MovieRS.API.Dtos.User
{
    public class TokenVerificationDto
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
