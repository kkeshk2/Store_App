using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void testGetAll_isValid()
    {
        Product myProduct = new Product();
        List<Product> productList = myProduct.GetAll();
        Assert.IsNotNull(productList);
    }

    [TestMethod]
    public void GetById()
    {
        int id = 1;
        Product myProduct = new Product();
        Product returnedProduct = myProduct.GetOne(1);
        Assert.AreEqual(id, returnedProduct.ProductId);
    }
}