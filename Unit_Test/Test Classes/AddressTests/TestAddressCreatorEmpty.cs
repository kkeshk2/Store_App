using Store_App.Models.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.AddressTests
{
    public class TestAddressCreatorEmpty : IAddressCreator
    {
        public IAddress GetAddress(int id, string name, string line1, string city, string state, string postal)
        {
            return new Address3Lines(id, name, line1, city, state, postal, new TestAddressDataContextEmpty(), new AddressValidator());
        }

        public IAddress GetAddress(int id, string name, string line1, string line2, string city, string state, string postal)
        {
            return new Address4Lines(id, name, line1, line2, city, state, postal, new TestAddressDataContextEmpty(), new AddressValidator());
        }
    }
}
