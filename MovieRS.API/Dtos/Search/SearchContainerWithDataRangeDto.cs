namespace MovieRS.API.Dtos.Search
{
    public class SearchContainerWithDataRangeDto<T> : SearchContainerDto<T>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
