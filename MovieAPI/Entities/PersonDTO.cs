using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Born { get; set; }

        [MaxLength(6)]
        public string? Gender { get; set; }
    }
}
