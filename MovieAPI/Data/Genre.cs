namespace MovieAPI.Data
{
    public class Genre
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<Movie> Movies { get; } = [];
    }
}
