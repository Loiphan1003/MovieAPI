using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class PersonUpdate
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateOnly Born { get; set; }

        [MaxLength(6)]
        public string? Gender { get; set; }
        public List<CastDTO> Casts { get; set; } = [];
    }
}
