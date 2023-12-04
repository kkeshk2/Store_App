namespace Store_App.Exceptions
{
    public class EmailTakenException : Exception
    {
        public EmailTakenException(string message) : base(message) { }
        public EmailTakenException(string message, Exception inner) : base(message, inner) { }
    }
}
