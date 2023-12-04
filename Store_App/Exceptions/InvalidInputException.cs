namespace Store_App.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
    }
}
