using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class PersonVM
    {
        public required string Name { get; set; }
        public DateTime? Born { get; set; }

        [MaxLength(6)]
        public string? Gender { get; set; }
    }
}
