namespace Store_App.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string message) : base(message) { }
        public AddressNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
