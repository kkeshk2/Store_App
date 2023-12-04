namespace Store_App.Models.InvoiceModel
{
    public interface IInvoiceCreator
    {
        public IInvoice CreateInvoice(int accountId, string creditCardString, string billingAddressString, string shippingAddressString);
    }
}
