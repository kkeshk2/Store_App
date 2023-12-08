using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.ProductTests;

namespace Unit_Test.Test_Classes.InvoiceTests
{
    public class TestInvoiceDataContext : IDataContext
    {
        public ISqlHelper GetConnection(string command)
        {
            return new TestInvoiceSqlHelper(command);
        }
    }
}
