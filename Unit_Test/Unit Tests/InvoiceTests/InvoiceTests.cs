using Store_App.Exceptions;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using Store_App.Models.InvoiceModel;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.AddressTests;
using Unit_Test.Test_Classes.CartTests;
using Unit_Test.Test_Classes.InvoiceTests;

namespace Unit_Test.Unit_Tests.InvoiceTests
{
    [TestClass]
    public class InvoiceTests
    {
        public static IAddress Address1 = GetAddress(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");
        public static IAddress Address2 = GetAddress(2, "John Doe", "100 Main Street", "Apt 101", "Omaha", "NE", "68500");
        public static List<ICartProduct> Products1 = new List<ICartProduct>()
        {
            {new TestCartProductCreator().GetCartProduct(1, 1, 100M) }
        };
        public static List<ICartProduct> Products2 = new List<ICartProduct>()
        {
            {new TestCartProductCreator().GetCartProduct(1, 1, 200M) }
        };

        public static IAddress GetAddress(int id, string name, string line1, string line2, string city, string state, string postal)
        {
            var creator = new TestAddressCreator();
            return creator.GetAddress(id, name, line1, line2, city, state, postal);
        }

        [TestMethod]
        public void TestInvoiceEqual1()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            Assert.AreEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual1()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(2, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual3()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 2, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual4()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 1, 2, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual5()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 200M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual6()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "2000", Address1, Address1, "1000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestInvoiceNotEqual7()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, DateTime.MinValue, "1000", Address1, Address1, "2000");
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestAccessInvoice()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products1, new DateTime(2000, 1, 1), "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice();
            invoice2.AccessInvoice(1, 1);
            Assert.AreEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestAccessInvoiceNotEqual()
        {
            IInvoice invoice1 = new TestInvoiceCreator().GetInvoice(1, 1, 1, 100M, Products2, new DateTime(2000, 1, 1), "1000", Address1, Address1, "1000");
            IInvoice invoice2 = new TestInvoiceCreator().GetInvoice();
            invoice2.AccessInvoice(1, 1);
            Assert.AreNotEqual(invoice1, invoice2);
        }

        [TestMethod]
        public void TestAccessInvoiceThrowsException()
        {
            IInvoice invoice = new TestInvoiceCreatorEmpty().GetInvoice();
            Assert.ThrowsException<InvoiceNotFoundException>(() => invoice.AccessInvoice(1, 1));
        }

        [TestMethod]
        public void TestInvoiceProcessor()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            Assert.AreEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual1()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(2, 1, 1, 100M, Products1, "1000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual2()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 2, 1, 100M, Products1, "1000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual3()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 2, 100M, Products1, "1000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual4()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 200M, Products1, "1000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual5()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products2, "1000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual6()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "2000", Address1, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual7()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address2, Address1);
            Assert.AreNotEqual(processor1, processor2);
        }

        [TestMethod]
        public void TestInvoiceProcessorNotEqual8()
        {
            IInvoiceProcessor processor1 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address1);
            IInvoiceProcessor processor2 = new TestInvoiceProcessorCreator().GetInvoiceCreator(1, 1, 1, 100M, Products1, "1000", Address1, Address2);
            Assert.AreNotEqual(processor1, processor2);
        }
    }
}
