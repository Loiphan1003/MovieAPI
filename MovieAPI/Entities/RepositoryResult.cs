namespace MovieAPI.Entities
{
    public class RepositoryResult
    {
        public bool Success {  get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }

        public RepositoryResult(bool success, string statusCode ,string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }
    }
}
