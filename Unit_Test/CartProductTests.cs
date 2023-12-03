using Store_App.Models.Classes;
using Store_App.Models.Interfaces;

namespace Unit_Test;

[TestClass]
public class CartProductTests
{
    [TestMethod]
    public void TestGetOne()
    {
        CartProduct cartProd = new CartProduct();
        int cartId = 1;
        int productId = 1;
        CartProduct returnedCartProduct = cartProd.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);

        cartId = 1;
        productId = 2;
        returnedCartProduct = cartProd.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);

        cartId = 2;
        productId = 1;
        returnedCartProduct = cartProd.GetOne(cartId, productId);
        Assert.IsNotNull(returnedCartProduct);
        Assert.AreEqual(cartId, returnedCartProduct.CartId);
        Assert.AreEqual(productId, returnedCartProduct.ProductId);
    }

    [TestMethod]
    public void TestGetOne_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetOne(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetOne(1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetOne(-2, 2));
    }

    [TestMethod]
    public void TestGetCartProductsBasedOnCart()
    {
        CartProduct cartProd = new CartProduct();
        int cartId = 1;
        List<CartProduct> returnedCartProductList = cartProd.GetCartProductsBasedOnCart(cartId);
        Assert.IsNotNull(returnedCartProductList);
        Assert.AreEqual(2, returnedCartProductList.Count());

        cartId = 2;
        returnedCartProductList = cartProd.GetCartProductsBasedOnCart(cartId);
        Assert.IsNotNull(returnedCartProductList);
        Assert.AreEqual(1, returnedCartProductList.Count());
    }

    [TestMethod]
    public void TestGetCartProductsBasedOnCart_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetCartProductsBasedOnCart(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetCartProductsBasedOnCart(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.GetCartProductsBasedOnCart(0));
    }

    [TestMethod]
    public void TestUpdateCartProductQuantity_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.UpdateCartProductQuantity(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.UpdateCartProductQuantity(1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.UpdateCartProductQuantity(0, 0));
    }

    [TestMethod]
    public void TestAddOneToCartProductDatabase_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.AddOneToCartProductDatabase(-1, 1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.AddOneToCartProductDatabase(1, -1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.AddOneToCartProductDatabase(1, 1, -1));
    }

    [TestMethod]
    public void TestDeleteFromCartProductDatabase_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteFromCartProductDatabase(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteFromCartProductDatabase(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteFromCartProductDatabase(0));
    }

    [TestMethod]
    public void TestDeleteCartProductsForOneCart_isValid()
    {
        CartProduct cartProd = new CartProduct();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteCartProductsForOneCart(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteCartProductsForOneCart(-2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cartProd.DeleteCartProductsForOneCart(0));
    }
}