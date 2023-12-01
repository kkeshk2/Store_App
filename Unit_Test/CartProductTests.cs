using Store_App.Models.Classes;

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
}