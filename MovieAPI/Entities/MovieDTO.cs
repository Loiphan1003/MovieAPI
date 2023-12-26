namespace MovieAPI.Entities
{
    public class MovieDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public int Budget { get; set; }
        public string? DateRelease { get; set; }
        public string? Runtime { get; set; }
        public float IMDbRate { get; set; }

        public List<GenreDTO> Genres { get; set; } = null!;
        public List<CastDTO> Casts { get; set; } = null!;
    }
}
