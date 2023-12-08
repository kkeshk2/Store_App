using Store_App.Models.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Test_Classes.ProductTests;

namespace Unit_Test.Test_Classes.CartTests
{
    internal class TestCartProductCreator : ICartProductCreator
    {
        public ICartProduct GetCartProduct(int productId, int quantity)
        {
            return new CartProduct(productId, quantity, new TestProductCreator());
        }

        public ICartProduct GetCartProduct(int productId, int quantity, decimal price)
        {
            return new CartProduct(productId, quantity, price, new TestProductCreator());
        }
    }
}
