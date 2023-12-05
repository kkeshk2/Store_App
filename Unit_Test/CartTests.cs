using Store_App.Models.CartModel;

namespace Unit_Test;

[TestClass]
public class CartTests
{
    [TestMethod]
    public void TestAccessCartEqual()
    {
        ICart cart1 = new Cart();
        ICart cart2 = new Cart();
        cart1.AccessCart(1);
        cart2.AccessCart(1);
        Assert.AreEqual(cart1, cart2);
    }

    [TestMethod]
    public void TestAccessCartNotEqual()
    {
        ICart cart1 = new Cart();
        ICart cart2 = new Cart();
        cart1.AccessCart(1);
        cart2.AccessCart(2);
        Assert.AreNotEqual(cart1, cart2);
    }

    [TestMethod]
    public void TestAddToCart()
    {
        ICart cart = new Cart();
        cart.AccessCart(1);
        cart.AddToCart(3, 1);
        List<ICartProduct> cartProducts = cart.GetCartProducts();
        bool flag = false;
        for (int i = 0; i < cartProducts.Count; i++)
        {
            if (cartProducts[i].GetProductId() == 3 && cartProducts[i].GetQuantity() == 1)
            {
                flag = true; break;
            }
        }
        Assert.IsTrue(flag);
    }

    [TestMethod]
    public void TestCalculateTotal()
    {
        Cart cart = new Cart();
        cart.AccessCart(2);
        decimal totalPrice = 1199.97m;
        Assert.AreEqual(totalPrice, cart.GetTotal());
    }

    [TestMethod]
    public void TestDeleteItem()
    {
        ICart cart = new Cart();
        cart.AccessCart(1);
        cart.DeleteItem(2);
        List<ICartProduct> cartProducts = cart.GetCartProducts();
        bool flag = true;
        for (int i = 0; i < cartProducts.Count; i++)
        {
            if (cartProducts[i].GetProductId() == 2)
            {
                flag = false; break;
            }
        }
        Assert.IsTrue(flag);
    }
}