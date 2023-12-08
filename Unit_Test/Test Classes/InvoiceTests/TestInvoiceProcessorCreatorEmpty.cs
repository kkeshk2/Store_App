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
    public class TestInvoiceProcessorCreatorEmpty : IInvoiceProcessorCreator
    {
        public IInvoiceProcessor GetInvoiceCreator()
        {
            return new InvoiceProcessor(new TestAddressFactoryCreator(), new TestCartCreator(), new CreditCardValidator(), new TestInvoiceDataContextEmpty(), new TestInvoiceCreator());
        }

        public IInvoiceProcessor GetInvoiceCreator(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, string creditCard, IAddress billingAddress, IAddress shippingAddress)
        {
            return new InvoiceProcessor(invoiceId, accountId, size, total, products, creditCard, billingAddress, shippingAddress, new TestAddressFactoryCreator(), new TestCartCreator(), new CreditCardValidator(), new TestInvoiceDataContextEmpty(), new TestInvoiceCreator());
        }
    }
}
