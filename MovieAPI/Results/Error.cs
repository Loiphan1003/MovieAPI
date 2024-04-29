namespace MovieAPI.Result
{
    public sealed record Error(string code, string description)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty);
    }
}