using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class PersonUpdate
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly Born { get; set; }
        public required string Nationality { get; set; }

    }
}
