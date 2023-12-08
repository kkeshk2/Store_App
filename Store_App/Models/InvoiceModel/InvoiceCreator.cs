using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceCreator : IInvoiceCreator
    {
        public IInvoice GetInvoice()
        {
            return new Invoice(new AddressFactoryCreator(), new CartProductCreator(), new DataContext());
        }

        public IInvoice GetInvoice(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, DateTime dateTime, string creditCard, IAddress billingAddress, IAddress shippingAddress, string trackingNumber)
        {
            return new Invoice(invoiceId, accountId, size, total, products, dateTime, creditCard, billingAddress, shippingAddress, trackingNumber, new AddressFactoryCreator(), new CartProductCreator(), new DataContext());
        }
    }
}
