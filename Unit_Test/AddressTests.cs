using Store_App.Models.AddressModel;

namespace Unit_Test
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void TestAddress3LinesEqualsOverride1()
        {
            IAddress address1 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            IAddress address2 = new Address3Lines(1, "John Smith", "123 Main St", "Lincoln", "NE", "68500");
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddress3LinesEqualsOverride2()
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
        public void TestAddress4LinesEqualsOverride1()
        {
            IAddress address1 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address2 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddress4LinesEqualsOverride2()
        {
            IAddress address1 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address2 = new Address4Lines(2, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address3 = new Address4Lines(1, "Jack Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address4 = new Address4Lines(1, "John Smith", "124 Main St", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address5 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 201", "Lincoln", "NE", "68500");
            IAddress address6 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Omaha", "NE", "68500");
            IAddress address7 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "IA", "68500");
            IAddress address8 = new Address4Lines(1, "John Smith", "123 Main St", "Apt 101", "Lincoln", "NE", "68501");
            Assert.AreNotEqual(address1, address2);
            Assert.AreNotEqual(address1, address3);
            Assert.AreNotEqual(address1, address4);
            Assert.AreNotEqual(address1, address5);
            Assert.AreNotEqual(address1, address6);
            Assert.AreNotEqual(address1, address7);
            Assert.AreNotEqual(address1, address8);
        }
    }
}
