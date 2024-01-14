namespace MovieAPI.Entities
{
    public class MovieUpdate
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public int Budget { get; set; }
        public DateTime? DateRelease { get; set; }
        public string? Runtime { get; set; }
        public float IMDbRate { get; set; }
    }
}
