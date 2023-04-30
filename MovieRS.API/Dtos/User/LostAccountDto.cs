namespace MovieRS.API.Dtos.User
{
    public class LostAccountDto
    {
        public string Email { get; set; } = null!;
        public string? Code { get; set; }
    }
}
