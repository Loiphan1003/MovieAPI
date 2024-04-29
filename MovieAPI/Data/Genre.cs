﻿namespace MovieAPI.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Movie> Movies { get; } = [];
    }
}
