namespace MovieRS.API.Dtos.Movie
{
    public class SeachContainerWithIdDto<T> : SearchContainerDto<T>
    {
        public int Id { get; set; }
    }
}
