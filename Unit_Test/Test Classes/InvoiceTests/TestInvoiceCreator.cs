using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using Store_App.Models.InvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.AddressTests;
using Unit_Test.Test_Classes.CartTests;

namespace Unit_Test.Test_Classes.InvoiceTests
{
    public class TestInvoiceCreator : IInvoiceCreator
    {
        public IInvoice GetInvoice()
        {
            return new Invoice(new TestAddressFactoryCreator(), new TestCartProductCreator(), new TestInvoiceDataContext());
        }

        public IInvoice GetInvoice(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> cartProducts, DateTime dateTime, string creditCard, IAddress billingAddress, IAddress shippingAddress, string trackingNumber)
        {
            return new Invoice(invoiceId, accountId, size, total, cartProducts, dateTime, creditCard, billingAddress, shippingAddress, trackingNumber, new TestAddressFactoryCreator(), new TestCartProductCreator(), new TestInvoiceDataContext());
        }
    }
}
