﻿using Newtonsoft.Json;

namespace MovieRS.API.Dtos.Search
{
    public class SearchCollectionDto
    {
        public string? BackdropPath { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PosterPath { get; set; }
    }
}
