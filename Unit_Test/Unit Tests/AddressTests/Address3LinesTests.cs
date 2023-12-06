using Store_App.Helpers;
using Store_App.Models.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.DBScripts;

namespace Unit_Test.Unit_Tests.AddressTests
{
    [TestClass]
    public class Address3LinesTests
    {
        [TestInitialize]
        public void Initialize()
        {
            SqlHelper.SetTesting(true);
            using (ITestDatabaseBuilder builder = new TestDatabaseBuilder())
            {
                builder.ClearDB();
                builder.CreateDB();
                builder.PopulateDB();
            }
        }

        [TestMethod]
        public void TestEqualsOverridePositive()
        {
            IAddress address1 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address2 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestEqualsOverrideNegative()
        {
            IAddress address1 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address2 = new Address3Lines(2, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address3 = new Address3Lines(1, "Jack Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address4 = new Address3Lines(1, "John Smith", "124 Main St", "Lincoln", "NE", "68500");
            IAddress address5 = new Address3Lines(1, "John Smith", "123 Main St", "Omaha", "NE", "68500");
            IAddress address6 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "IA", "68500");
            IAddress address7 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68501");
            Assert.AreNotEqual(address1, address2);
            Assert.AreNotEqual(address1, address3);
            Assert.AreNotEqual(address1, address4);
            Assert.AreNotEqual(address1, address5);
            Assert.AreNotEqual(address1, address6);
            Assert.AreNotEqual(address1, address7);
        }

        [TestMethod]
        public void TestAddAddressNewAddress()
        {
            IAddress address1 = new Address3Lines(-1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address2 = new Address3Lines(3, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            address1.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressExistingAddress()
        {
            IAddress address1 = new Address3Lines(-1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address2 = new Address3Lines(3, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            address1.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestGetAddressIdPositive()
        {
            IAddress address = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            int addressId = address.GetAddressId();
            Assert.AreEqual(1, addressId);
        }

        [TestMethod]
        public void TestGetAddressIdNegative()
        {
            IAddress address = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            int addressId = address.GetAddressId();
            Assert.IsFalse(addressId > 1);
            Assert.IsFalse(addressId < 1);
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (ITestDatabaseBuilder builder = new TestDatabaseBuilder())
            {
                builder.ClearDB();
            }
            SqlHelper.SetTesting(false);
        }
    }
}
