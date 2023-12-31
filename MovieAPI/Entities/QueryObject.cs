﻿namespace MovieAPI.Entities
{
    public class QueryObject
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
