using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.AddressTests;

namespace Unit_Test.Unit_Tests.AddressTests
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void TestAddressEqaul()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul1()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul2()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "Jack Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul3()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "200 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul4()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 201", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul5()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul6()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Omaha", "NE", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul7()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "IA", "68500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressNotEqaul8()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "78500");
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressEqual1()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressEqual2()
        {
            var address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressEqual3()
        {
            var address1 = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressEqual4()
        {
            var address1 = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressNotEqual1()
        {
            var address1 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressNotEqual2()
        {
            var address1 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreator().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressNotEqual3()
        {
            var address1 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressNotEqual4()
        {
            var address1 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var address2 = new TestAddressCreatorEmpty().GetAddress(2, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            address2.AddAddress();
            Assert.AreNotEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddAddressIdEqual()
        {
            var expected = 1;
            var address = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var actual = address.GetAddressId();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddAddressIdNotEqual()
        {
            var expected = 2;
            var address = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            var actual = address.GetAddressId();
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddAddressIdEqual2()
        {
            var expected = 1;
            var address = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var actual = address.GetAddressId();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddAddressIdNotEqual2()
        {
            var expected = 2;
            var address = new TestAddressCreatorEmpty().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            var actual = address.GetAddressId();
            Assert.AreNotEqual(expected, actual);
        }
    }
}
