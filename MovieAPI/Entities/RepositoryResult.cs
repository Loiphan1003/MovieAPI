namespace MovieAPI.Entities
{
    public class RepositoryResult
    {
        public bool Success {  get; set; }
        public string Message { get; set; }

        public RepositoryResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
