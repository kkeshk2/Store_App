using Store_App.Models.CartModel;

namespace Unit_Test;

[TestClass]
public class CartProductTests
{
    [TestMethod]
    public void TestGetProductId()
    {
        int productId = 1;
        CartProduct cartProd = new CartProduct(1, 2);
        Assert.IsNotNull(cartProd.GetProductId());
        Assert.AreEqual(productId, cartProd.GetProductId());

        productId = 2;
        cartProd = new CartProduct(2, 1);
        Assert.IsNotNull(cartProd.GetProductId());
        Assert.AreEqual(productId, cartProd.GetProductId());

        productId = 1;
        cartProd = new CartProduct(1, 3);
        Assert.IsNotNull(cartProd.GetProductId());
        Assert.AreEqual(productId, cartProd.GetProductId());
    }

    [TestMethod]
    public void TestGetPrice()
    {
        CartProduct cartProd = new CartProduct(1, 2, 499.99m);
        Assert.IsNotNull(cartProd.GetPrice());
        Assert.AreEqual(499.99m, cartProd.GetPrice());

        cartProd = new CartProduct(2, 1, 349.99m);
        Assert.IsNotNull(cartProd.GetPrice());
        Assert.AreEqual(349.99m, cartProd.GetPrice());

        cartProd = new CartProduct(1, 3, 499.99m);
        Assert.IsNotNull(cartProd.GetPrice());
        Assert.AreEqual(499.99m, cartProd.GetPrice());
    }

    [TestMethod]
    public void TestGetQuantity()
    {
        CartProduct cartProd = new CartProduct(1, 2);
        Assert.IsNotNull(cartProd.GetQuantity());
        Assert.AreEqual(2,  cartProd.GetQuantity());

        cartProd = new CartProduct(2, 1);
        Assert.IsNotNull(cartProd.GetQuantity());
        Assert.AreEqual(1, cartProd.GetQuantity());

        cartProd = new CartProduct(1, 3);
        Assert.IsNotNull(cartProd.GetQuantity());
        Assert.AreEqual(3, cartProd.GetQuantity());
    }
}