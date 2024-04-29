namespace MovieAPI.Entities.ViewModels
{
    public class PersonAdd
    {
        public required string Name { get; set; }
        public DateOnly Born { get; set; }
        public required string Nationality { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Nationality);
        }

    }
}