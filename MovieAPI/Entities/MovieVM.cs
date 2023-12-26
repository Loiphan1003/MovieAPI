namespace MovieAPI.Entities
{
    public class MovieVM
    {
        public required string Title { get; set; }
        public int Budget { get; set; }
        public string? DateRelease { get; set; }
        public float IMDbRate { get; set; }
    }
}
