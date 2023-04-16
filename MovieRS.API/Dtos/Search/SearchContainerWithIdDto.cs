namespace MovieRS.API.Dtos.Search
{
    public class SearchContainerWithIdDto<T> : SearchContainerDto<T>
    {
        public int Id { get; set; }
    }
}
