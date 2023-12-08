using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;

namespace Store_App.Models.InvoiceModel
{
    public interface IInvoiceProcessorCreator
    {
        public IInvoiceProcessor GetInvoiceCreator();
        public IInvoiceProcessor GetInvoiceCreator(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, string creditCard, IAddress billingAddress, IAddress shippingAddress);
    }
}
