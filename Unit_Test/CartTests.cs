using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class CartTests
{
    [TestMethod]
    public void TestGetOneBasedOnAccountId_isValid()
    {
        int accountId = 1;
        Cart myCart = new Cart();
        Cart returnedCart = myCart.GetOneBasedOnAccountId(accountId);
        Assert.IsNotNull(returnedCart);

        accountId = 2;
        returnedCart = myCart.GetOneBasedOnAccountId(accountId);
        Assert.IsNotNull(returnedCart);
    }

    [TestMethod]
    public void TestGetOneBasedOnAccountId()
    {
        int accountId = 1;
        Cart myCart = new Cart();
        Cart returnedCart = myCart.GetOneBasedOnAccountId(accountId);
        Assert.AreEqual(accountId, returnedCart.AccountId);
        
        accountId = 2;
        returnedCart = myCart.GetOneBasedOnAccountId(accountId);
        Assert.AreEqual(accountId, returnedCart.AccountId);
    }

    [TestMethod]
    public void TestAddToCart_isValid()
    {
        Cart myCart = new Cart();
        Cart returnedCart = myCart.GetOneBasedOnAccountId(1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => returnedCart.AddToCart(returnedCart, 1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => returnedCart.AddToCart(returnedCart, -1, 1));
        Assert.ThrowsException<ArgumentNullException>(() => returnedCart.AddToCart(null, 1, 1));

        returnedCart = myCart.GetOneBasedOnAccountId(2);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => returnedCart.AddToCart(returnedCart, 2, 0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => returnedCart.AddToCart(returnedCart, 0, 4));
        Assert.ThrowsException<ArgumentNullException>(() => returnedCart.AddToCart(null, 3, 5));

    }
}