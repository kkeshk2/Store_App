namespace Store_App.Exceptions
{
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(string message) : base(message) { }
        public InvoiceNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
