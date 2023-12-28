namespace MovieAPI.Data
{
    public class Movie
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public int Budget { get; set; }
        public DateTime? DateRelease { get; set; }
        public string? Runtime { get; set; }
        public float IMDbRate { get; set; }

        public List<Genre> Genres { get; set; } = new();
        public List<MovieGenre> MovieGenres { get; set; } = new();

        public List<Person> Persons { get; set; } = new();
        public List<Cast> Casts { get; set; } = new();

    }
}
