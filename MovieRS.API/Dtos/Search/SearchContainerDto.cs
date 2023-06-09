﻿namespace MovieRS.API.Dtos.Search
{
    public class SearchContainerDto<T>
    {
        public int Page { get; set; }
        public IList<T>? Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
