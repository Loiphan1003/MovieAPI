namespace MovieAPI.Entities
{
    public class MovieVM
    {
        public required string Title { get; set; }
        public int Budget { get; set; }
        public DateOnly Release { get; set; }
        public string? Runtime { get; set; }
        public string? Poster { get; set; }
        public string? Trailer { get; set; }
        public float IMDbRate { get; set; }
    }
}
