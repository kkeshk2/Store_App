using Store_App.Models.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.AddressTests
{
    public class TestAddressFactoryCreatorEmpty
    {
        public IAddressFactory GetAddressFactory()
        {
            return new AddressFactory(new TestAddressDataContextEmpty(), new TestAddressCreator());
        }

        public IAddressFactory GetAddressFactory(int id, string name, string line1, string? line2, string city, string state, string postal)
        {
            return new AddressFactory(id, name, line1, line2, city, state, postal, new TestAddressDataContextEmpty(), new TestAddressCreator());
        }
    }
}
