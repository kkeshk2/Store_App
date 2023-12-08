using Store_App.Models.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.CartTests
{
    public class TestCartCreator : ICartCreator
    {
        public ICart GetCart()
        {
            return new Cart(new TestCartProductCreator(), new TestCartDataContext());
        }
    }
}
