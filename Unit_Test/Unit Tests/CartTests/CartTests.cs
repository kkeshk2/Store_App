using Store_App.Models.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.CartTests;

namespace Unit_Test.Unit_Tests.CartTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CartEqual1()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void CartEqual2()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            cart2.AccessCart(1);
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void CartNotEqual1()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            Assert.AreNotEqual(cart1, cart2);
        }

        [TestMethod]
        public void CartNotEqual2()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart2.AccessCart(1);
            Assert.AreNotEqual(cart1, cart2);
        }

        [TestMethod]
        public void TestAddToCart()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            cart2.AccessCart(1);
            cart2.AddToCart(1, 2);
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void TestDeleteFromCart()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            cart2.AccessCart(1);
            cart2.DeleteItem(1);
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void TestClearCart()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            cart2.AccessCart(1);
            cart2.ClearCart();
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void TestUpdateCart()
        {
            ICart cart1 = new TestCartCreator().GetCart();
            ICart cart2 = new TestCartCreator().GetCart();
            cart1.AccessCart(1);
            cart2.AccessCart(1);
            cart2.UpdateCart(1, 9);
            Assert.AreEqual(cart1, cart2);
        }

        [TestMethod]
        public void CartProductEqual1()
        {
            ICartProduct cartProduct1 = new TestCartProductCreator().GetCartProduct(1, 1);
            ICartProduct cartProduct2 = new TestCartProductCreator().GetCartProduct(1, 1);
            Assert.AreEqual(cartProduct1, cartProduct2);
        }

        [TestMethod]
        public void CartProductEqual2()
        {
            ICartProduct cartProduct1 = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            ICartProduct cartProduct2 = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            Assert.AreEqual(cartProduct1, cartProduct2);
        }

        [TestMethod]
        public void CartProductEqual3()
        {
            ICartProduct cartProduct1 = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            ICartProduct cartProduct2 = new TestCartProductCreator().GetCartProduct(0, 1, 10.0M);
            Assert.AreEqual(cartProduct1, cartProduct2);
        }

        [TestMethod]
        public void CartProductNotEqual2()
        {
            ICartProduct cartProduct1 = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            ICartProduct cartProduct2 = new TestCartProductCreator().GetCartProduct(1, 0, 10.0M);
            Assert.AreNotEqual(cartProduct1, cartProduct2);
        }

        [TestMethod]
        public void CartProductNotEqual3()
        {
            ICartProduct cartProduct1 = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            ICartProduct cartProduct2 = new TestCartProductCreator().GetCartProduct(1, 1, 5.0M);
            Assert.AreNotEqual(cartProduct1, cartProduct2);
        }

        [TestMethod]
        public void TestGetProductId()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(1, 1, 10.0M);
            Assert.AreEqual(1, cartProduct.GetProductId());
        }

        [TestMethod]
        public void TestGetProductId2()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(2, 1, 10.0M);
            Assert.AreEqual(1, cartProduct.GetProductId());
        }

        [TestMethod]
        public void TestGetQuantity1()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(2, 1, 10.0M);
            Assert.AreEqual(1, cartProduct.GetQuantity());
        }

        [TestMethod]
        public void TestGetQuantity2()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(2, 4, 10.0M);
            Assert.AreEqual(4, cartProduct.GetQuantity());
        }

        [TestMethod]
        public void TestGetPrice1()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(2, 1, 10.0M);
            Assert.AreEqual(10.0M, cartProduct.GetPrice());
        }

        [TestMethod]
        public void TestGetPrice2()
        {
            ICartProduct cartProduct = new TestCartProductCreator().GetCartProduct(2, 1, 100.0M);
            Assert.AreEqual(100.0M, cartProduct.GetPrice());
        }
    }
}
