using System.Text.Json.Serialization;
using MovieAPI.Entities;

namespace MovieAPI.Data
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Budget { get; set; }
        public DateOnly Release { get; set; }
        public string? Runtime { get; set; }
        public float IMDbRate { get; set; }
        public string? Trailer { get; set; }
        public string? Poster { get; set; }
        public List<Genre> Genres { get; } = [];

        [JsonIgnore]
        public List<Cast> Casts { get; } = [];

        [JsonIgnore]
        public List<Person> People { get; } = [];

    }
}
