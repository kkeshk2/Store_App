using Store_App.Models.Classes;
using Store_App.Models.Interfaces;

namespace Unit_Test;

[TestClass]
public class CartProductTests
{
    [TestMethod]
    public void TestGetOne()
    {
        int cartId = 1;
        int productId = 1;
        CartProduct returnedCartProduct = CartProduct.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);

        cartId = 1;
        productId = 2;
        returnedCartProduct = CartProduct.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);

        cartId = 2;
        productId = 1;
        returnedCartProduct = CartProduct.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);
    }

    [TestMethod]
    public void TestGetOne_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetOne(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetOne(1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetOne(-2, 2));
    }

    [TestMethod]
    public void TestGetCartProductsBasedOnCart()
    {
        int cartId = 1;
        List<CartProduct> returnedCartProductList = CartProduct.GetCartProductsBasedOnCart(cartId);
        Assert.IsNotNull(returnedCartProductList);
        Assert.AreEqual(2, returnedCartProductList.Count());

        cartId = 2;
        returnedCartProductList = CartProduct.GetCartProductsBasedOnCart(cartId);
        Assert.IsNotNull(returnedCartProductList);
        Assert.AreEqual(1, returnedCartProductList.Count());
    }

    [TestMethod]
    public void TestGetCartProductsBasedOnCart_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetCartProductsBasedOnCart(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetCartProductsBasedOnCart(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.GetCartProductsBasedOnCart(0));
    }

    [TestMethod]
    public void TestUpdateCartProductQuantity_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.UpdateCartProductQuantity(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.UpdateCartProductQuantity(1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.UpdateCartProductQuantity(0, 0));
    }

    [TestMethod]
    public void TestAddOneToCartProductDatabase_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.AddOneToCartProductDatabase(-1, 1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.AddOneToCartProductDatabase(1, -1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.AddOneToCartProductDatabase(1, 1, -1));
    }

    [TestMethod]
    public void TestDeleteFromCartProductDatabase_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteFromCartProductDatabase(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteFromCartProductDatabase(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteFromCartProductDatabase(0));
    }

    [TestMethod]
    public void TestDeleteCartProductsForOneCart_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteCartProductsForOneCart(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteCartProductsForOneCart(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CartProduct.DeleteCartProductsForOneCart(0));
    }
}