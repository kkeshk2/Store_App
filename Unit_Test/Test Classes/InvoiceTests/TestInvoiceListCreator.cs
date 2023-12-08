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
    public class TestInvoiceListCreator : IInvoiceListCreator
    {
        public IInvoiceList GetInvoiceList()
        {
            return new InvoiceList(new TestInvoiceDataContext(), new TestInvoiceCreator());
        }    
    }
}
