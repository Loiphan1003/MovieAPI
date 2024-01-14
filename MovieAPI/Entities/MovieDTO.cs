using MovieAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class MovieDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int Budget { get; set; }
        public DateTime? DateRelease { get; set; }
        public string? Runtime { get; set; }
        public float IMDbRate { get; set; }

        public List<GenreDTO> Genres { get; set; } = null!;
        public List<PersonDTO> Persons { get; set; } = null!;
    }
}
