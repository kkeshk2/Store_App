using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void TestGetAll_isValid()
    {
        Product myProduct = new Product();
        List<Product> productList = myProduct.GetAll();
        Assert.IsNotNull(productList);
    }

    [TestMethod]
    public void GetById_isValid()
    {
        Product myProduct = new Product();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => myProduct.GetOne(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => myProduct.GetOne(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => myProduct.GetOne(-2));
    }

    [TestMethod]
    public void GetById()
    {
        int id = 1;
        Product myProduct = new Product();
        Product returnedProduct = myProduct.GetOne(1);
        Assert.AreEqual(id, returnedProduct.ProductId);

        id = 2;
        returnedProduct = myProduct.GetOne(2);
        Assert.AreEqual(id, returnedProduct.ProductId);

        id = 3;
        returnedProduct = myProduct.GetOne(3);
        Assert.AreEqual(id, returnedProduct.ProductId);
    }
}