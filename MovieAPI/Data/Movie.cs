namespace MovieAPI.Data
{
    public class Movie
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public int Budget { get; set; }
        public string DateRelease { get; set; }
        public float IMDbRate { get; set; }

        public List<Genre> Genres { get; set; } = new();
        public List<MovieGenre> MovieGenres { get; set; } = new();

    }
}
