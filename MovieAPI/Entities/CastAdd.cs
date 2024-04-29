namespace MovieAPI.Entities
{
    public class CastAdd
    {
        public required string PersonName { get; set; }
        public required string MovieName { get; set; }
        public required string Character { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(PersonName) && !string.IsNullOrEmpty(MovieName) && !string.IsNullOrEmpty(Character);
        }
    }
}
