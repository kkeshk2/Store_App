using Store_App.Exceptions;
using Store_App.Models.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.AddressTests;

namespace Unit_Test.Unit_Tests.AddressTests
{
    [TestClass]
    public class AddressFactoryTests
    {
        [TestMethod]
        public void TestAddressFactoryEqual1()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryEqual2()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", null, "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", null, "Lincoln", "NE", "68500");
            Assert.AreEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual1()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(2, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual2()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "Jack Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual3()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "200 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual4()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 201", "Lincoln", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual5()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", null, "Lincoln", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual6()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Omaha", "NE", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual7()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "IA", "68500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryNotEqual8()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "78500");
            Assert.AreNotEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryAccessAddress()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory();
            factory2.AccessAddress(3);
            Assert.AreEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryAccessAddressException()
        {
            IAddressFactory factory = new TestAddressFactoryCreatorEmpty().GetAddressFactory();
            Assert.ThrowsException<AddressNotFoundException>(() => factory.AccessAddress(3));
        }

        [TestMethod]
        public void TestAddressFactorySetAddress()
        {
            IAddressFactory factory1 = new TestAddressFactoryCreator().GetAddressFactory(0, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddressFactory factory2 = new TestAddressFactoryCreator().GetAddressFactory();
            factory2.SetAddress("John Smith\t100 Main Street\tApt 101\tLincoln\tNE\t68500");
            Assert.AreEqual(factory1, factory2);
        }

        [TestMethod]
        public void TestAddressFactoryCreate1()
        {
            IAddressFactory factory = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
            IAddress address2 = factory.Create();
            Assert.AreEqual(address1, address2);
        }

        [TestMethod]
        public void TestAddressFactoryCreate2()
        {
            IAddressFactory factory = new TestAddressFactoryCreator().GetAddressFactory(1, "John Smith", "100 Main Street", null, "Lincoln", "NE", "68500");
            IAddress address1 = new TestAddressCreator().GetAddress(1, "John Smith", "100 Main Street", "Lincoln", "NE", "68500");
            IAddress address2 = factory.Create();
            Assert.AreEqual(address1, address2);
        }
    }
}
