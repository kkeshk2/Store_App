using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceProcessorCreator : IInvoiceProcessorCreator
    {
        public IInvoiceProcessor GetInvoiceCreator()
        {
            return new InvoiceProcessor (new AddressFactoryCreator(), new CartCreator(), new CreditCardValidator(), new DataContext(), new InvoiceCreator());
        }

        public IInvoiceProcessor GetInvoiceCreator (int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, string creditCard, IAddress billingAddress, IAddress shippingAddress)
        {
            return new InvoiceProcessor(invoiceId, accountId, size, total, products, creditCard, billingAddress, shippingAddress, new AddressFactoryCreator(), new CartCreator(), new CreditCardValidator(), new DataContext(), new InvoiceCreator());
        }
    }
}
