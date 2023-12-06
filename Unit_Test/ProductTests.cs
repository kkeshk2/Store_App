using Store_App.Models.ProductModel;


namespace Unit_Test;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void TestAccessProductEqual()
    {
        IProduct product1 = new Product();
        IProduct product2 = new Product();
        product1.AccessProduct(1);
        product2.AccessProduct(1);
        Assert.AreEqual(product1, product2);
    }

    [TestMethod]
    public void TestAccessProductNotEqual()
    {
        IProduct product1 = new Product();
        IProduct product2 = new Product();
        product1.AccessProduct(1);
        product2.AccessProduct(2);
        Assert.AreNotEqual(product1, product2);
    }

    [TestMethod]
    public void TestAccessProductSale()
    {
        IProduct product1 = new Product();
        product1.AccessProduct(1);
        product1.AccessProductSale();
        decimal salePrice = 399.99m;
        Assert.AreEqual(salePrice, product1.GetPrice());
    }

    [TestMethod]
    public void TestGetProductId()
    {
        IProduct product1 = new Product();
        product1.AccessProduct(1);
        Assert.AreEqual(1, product1.GetProductId());
    }

    [TestMethod]
    public void TestAccessProductListEqual()
    {
        IProductList products1 = new ProductList();
        IProductList products2 = new ProductList();
        products1.AccessProductList();
        products2.AccessProductList();
        Assert.AreEqual(products1, products2);
    }
}