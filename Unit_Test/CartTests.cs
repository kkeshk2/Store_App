using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class CartTests
{
    [TestMethod]
    public void TestGetOneBasedOnAccountId_isValid()
    {
        int accountId = 1;
        Cart returnedCart = Cart.GetOneBasedOnAccountId(accountId);
        Assert.IsNotNull(returnedCart);

        accountId = 2;
        returnedCart = Cart.GetOneBasedOnAccountId(accountId);
        Assert.IsNotNull(returnedCart);
    }

    [TestMethod]
    public void TestGetOneBasedOnAccountId()
    {
        int accountId = 1;
        Cart returnedCart = Cart.GetOneBasedOnAccountId(accountId);
        Assert.AreEqual(accountId, returnedCart.AccountId);
        
        accountId = 2;
        returnedCart = Cart.GetOneBasedOnAccountId(accountId);
        Assert.AreEqual(accountId, returnedCart.AccountId);
    }

    [TestMethod]
    public void TestAddToCart_isValid()
    {
        Cart returnedCart = Cart.GetOneBasedOnAccountId(1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.AddToCart(returnedCart, 1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.AddToCart(returnedCart, -1, 1));
        Assert.ThrowsException<ArgumentNullException>(() => Cart.AddToCart(null, 1, 1));

        returnedCart = Cart.GetOneBasedOnAccountId(2);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.AddToCart(returnedCart, 2, 0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.AddToCart(returnedCart, 0, 4));
        Assert.ThrowsException<ArgumentNullException>(() => Cart.AddToCart(null, 3, 5));

    }

    [TestMethod]
    public void TestDeleteFromCart_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteFromCart(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteFromCart(1, -1));

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteFromCart(-2, 2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteFromCart(2, -3));
    }

    [TestMethod]
    public void TestDeleteAllFromCart_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteAllFromCart(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.DeleteAllFromCart(-2));
    }

    [TestMethod]
    public void TestGetTotalPrice_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.GetTotalPrice(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Cart.GetTotalPrice(-2));
    }

    [TestMethod]
    public void TestGetTotalPrice()
    {
        double totalPrice = 1349.97;
        Assert.AreEqual(totalPrice, Cart.GetTotalPrice(1));

        totalPrice = 1499.97;
        Assert.AreEqual(totalPrice, Cart.GetTotalPrice(2));
    }
}