namespace MovieAPI.Data
{
    public class Cast
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public required string Character { get; set; }

        public Movie Movie { get; set; } = null!;
        public Person Person { get; set; } = null!;
    }
}
