namespace MovieAPI.Data
{
    public class Cast
    {
        public Guid MovieId { get; set; }
        public Guid PersonId { get; set; }
        public required string Character { get; set; }

        public Movie Movie { get; set; } = null!;
        public Person Person { get; set; } = null!;
    }
}
