using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;

namespace Store_App.Models.InvoiceModel
{
    public interface IInvoiceCreator
    {
        public IInvoice GetInvoice();
        public IInvoice GetInvoice(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, DateTime dateTime, string creditCard, IAddress billingAddress, IAddress shippingAddress, string trackingNumber);
    }
}
