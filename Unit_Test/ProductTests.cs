using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void TestGetAll_isValid()
    {
        List<Product> productList = Product.GetAll();
        Assert.IsNotNull(productList);
    }

    [TestMethod]
    public void GetById_isValid()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Product.GetOne(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Product.GetOne(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Product.GetOne(-2));
    }

    [TestMethod]
    public void GetById()
    {
        int id = 1;
        Product returnedProduct = Product.GetOne(1);
        Assert.AreEqual(id, returnedProduct.ProductId);

        id = 2;
        returnedProduct = Product.GetOne(2);
        Assert.AreEqual(id, returnedProduct.ProductId);

        id = 3;
        returnedProduct = Product.GetOne(3);
        Assert.AreEqual(id, returnedProduct.ProductId);
    }
}