using Store_App.Exceptions;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Unit_Test.DBScripts;

namespace Unit_Test.Unit_Tests.AddressTests
{
    [TestClass]
    public class AddressFactoryTests
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
            IAddressFactory address1 = new AddressFactory(1, "John Smith", "123 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address2 = new AddressFactory(1, "John Smith", "123 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address3 = new AddressFactory(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory address4 = new AddressFactory(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreEqual(address1, address2);
            Assert.AreEqual(address3, address4);
        }

        [TestMethod]
        public void TestEqualsOverrideNegative()
        {
            IAddressFactory address1 = new AddressFactory(1, "John Smith", "123 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address2 = new AddressFactory(2, "John Smith", "123 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address3 = new AddressFactory(1, "Jack Smith", "123 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address4 = new AddressFactory(1, "John Smith", "124 Main St", null, "Lincoln", "NE", "68500");
            IAddressFactory address5 = new AddressFactory(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory address6 = new AddressFactory(1, "John Smith", "123 Main St", null, "Omaha", "NE", "68500");
            IAddressFactory address7 = new AddressFactory(1, "John Smith", "123 Main St", null, "Lincoln", "IA", "68500");
            IAddressFactory address8 = new AddressFactory(1, "John Smith", "123 Main St", null, "Lincoln", "NE", "68501");
            Assert.AreNotEqual(address1, address2);
            Assert.AreNotEqual(address1, address3);
            Assert.AreNotEqual(address1, address4);
            Assert.AreNotEqual(address1, address5);
            Assert.AreNotEqual(address5, address1);
            Assert.AreNotEqual(address1, address6);
            Assert.AreNotEqual(address1, address7);
            Assert.AreNotEqual(address1, address8);
        }

        [TestMethod]
        public void TestAccessAddress3Lines()
        {
            IAddressFactory address1 = new AddressFactory();
            IAddressFactory address2 = new AddressFactory(2, "Work Address", "456 Elm St", null, "City B", "NY", "54321");
            address1.AccessAddress(2);
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAccessAddress4Lines()
        {
            IAddressFactory address1 = new AddressFactory();
            IAddressFactory address2 = new AddressFactory(1, "Home Address", "123 Main St", "Apt 4B", "City A", "CA", "12345");
            address1.AccessAddress(1);
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAccessAddressException()
        {
            IAddressFactory address1 = new AddressFactory();
            Assert.ThrowsException<AddressNotFoundException>(() => address1.AccessAddress(3));
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
