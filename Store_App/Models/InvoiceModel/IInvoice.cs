namespace Store_App.Models.InvoiceModel
{
    public interface IInvoice
    {
        public void AccessInvoice(int invoiceId, int accountId);
    }
}