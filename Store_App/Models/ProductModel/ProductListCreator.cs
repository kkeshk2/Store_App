using Store_App.Helpers;

namespace Store_App.Models.ProductModel
{
    public class ProductListCreator : IProductListCreator
    {
        public IProductList GetProductList()
        {
            return new ProductList(new DataContext(), new ProductCreator());
        }
    }
}
