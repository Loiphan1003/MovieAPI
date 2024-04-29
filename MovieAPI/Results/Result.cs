namespace MovieAPI.Result
{
    public class Result<T>
    {
        public Result(bool isSuccess, Error error, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Error = error;
            Data = data;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; }
        public Error Error { get; }
        public T Data { get; }

        public static Result<T> Success(string message, T datta)
        {
            return new Result<T>(true, Error.None, message, datta);
        }
        public static Result<T> Failure(Error error)
        {
            return new Result<T>(false, error, string.Empty, default!);
        }

    }
}