namespace MovieRS.API.Dtos.User
{
    public class TokenDto<T>
    {
        public string Value { get; set; } = null!;
        public DateTime ExpiredAt;
        public T? User;
    }
}
