using Store_App.Exceptions;
using Store_App.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.ProductTests;

namespace Unit_Test.Unit_Tests.ProductTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestEqual ()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual1()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(2, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual2()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual3()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 75M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual4()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 15M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual5()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 2.5M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual6()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual7()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual8()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual9()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 1.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual10()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 1.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual11()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 1.0M, 10.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual12()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 50.0M, "Null Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual13()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Sku", "Null Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestNotEqual14()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Location");
            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void TestAccessProduct()
        {
            IProduct product1 = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            IProduct product2 = new TestProductCreator().GetProduct();
            product2.AccessProduct(1);
            Assert.AreEqual(product1, product2);
        }

        [TestMethod]
        public void TestAccessProductThrowsException()
        {
            IProduct product = new TestProductCreatorEmpty().GetProduct();
            Assert.ThrowsException<ProductNotFoundException>(() => product.AccessProduct(1));
        }

        [TestMethod]
        public void TestGetPrice()
        {
            IProduct product = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            var expected = 75M;
            var actual = product.GetPrice();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetPriceNotEqual()
        {
            IProduct product = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            var expected = 100M;
            var actual = product.GetPrice();
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetProductId()
        {
            IProduct product = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            var expected = 1;
            var actual = product.GetProductId();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetProductIdNotEqual()
        {
            IProduct product = new TestProductCreator().GetProduct(1, "Null Product", 100M, 25M, 5.0M, "Null Manufacturer", "Null Description", "Null Category", 5.0M, 5.0M, 5.0M, 10.0M, "Null Sku", "Null Location");
            var expected = 2;
            var actual = product.GetProductId();
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void TestProductListEqual()
        {
            IProductList list1 = new TestProductListCreator().GetProductList();
            IProductList list2 = new TestProductListCreator().GetProductList();
            Assert.AreEqual (list1, list2);
        }

        [TestMethod]
        public void TestProductListNotEqual1()
        {
            IProductList list1 = new TestProductListCreator().GetProductList();
            IProductList list2 = new TestProductListCreator().GetProductList();
            list2.AccessProductList();
            Assert.AreNotEqual(list1, list2);
        }

        [TestMethod]
        public void TestProductListNotEqual2()
        {
            IProductList list1 = new TestProductListCreatorEmpty().GetProductList();
            IProductList list2 = new TestProductListCreator().GetProductList();
            list1.AccessProductList();
            list2.AccessProductList();
            Assert.AreNotEqual(list1, list2);
        }

        [TestMethod]
        public void TestProductListEqual2()
        {
            IProductList list1 = new TestProductListCreator().GetProductList();
            IProductList list2 = new TestProductListCreator().GetProductList();
            list1.AccessProductList();
            list2.AccessProductList();
            Assert.AreEqual(list1, list2);
        }
    }
}
