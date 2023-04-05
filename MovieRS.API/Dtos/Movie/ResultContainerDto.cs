namespace MovieRS.API.Dtos.Movie
{
    public class ResultContainerDto<T>
    {
        public int Id { get; set; }
        public IList<T>? Results { get; set; }
    }
}
