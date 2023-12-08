using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.AccountTests;

namespace Unit_Test.Test_Classes.AddressTests
{
    public class TestAddressDataContext : IDataContext
    {
        public ISqlHelper GetConnection(string command)
        {
            return new TestAddressSqlHelper(command);
        }
    }
}
