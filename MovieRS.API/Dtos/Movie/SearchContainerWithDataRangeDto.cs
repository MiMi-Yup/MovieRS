namespace MovieRS.API.Dtos.Movie
{
    public class SearchContainerWithDataRangeDto<T> : SearchContainerDto<T>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
