namespace Store_App.Models.InvoiceModel
{
    public interface IInvoiceProcessor
    {
        public IInvoice CreateInvoice(int accountId, string creditCardString, string billingAddressString, string shippingAddressString);
    }
}
