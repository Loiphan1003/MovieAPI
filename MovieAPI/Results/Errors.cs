namespace MovieAPI.Result
{
    public class Errors
    {
        public enum Code
        {
            AlreadyCreate,
            NotFound,
            Missing,
            InternalError
        }

        public static Error AlreadyCreate(string message)
        {
            return new Error(Code.AlreadyCreate.ToString(), message);
        }
        public static Error NotFound(string message)
        {
            return new Error(Code.NotFound.ToString(), message);
        }
        public static Error MissingData(string message)
        {
            return new Error(Code.Missing.ToString(), message);
        }
        public static Error InternalError(string message)
        {
            return new Error(Code.Missing.ToString(), message);

        }
    }
}