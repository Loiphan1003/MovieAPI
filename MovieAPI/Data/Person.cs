using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieAPI.Data
{
    public class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly Born { get; set; }
        public required string Nationality { get; set; }

        [JsonIgnore]
        public List<Movie> Movies { get; } = [];

        [JsonIgnore]
        public List<Cast> Casts { get; } = [];

    }
}
