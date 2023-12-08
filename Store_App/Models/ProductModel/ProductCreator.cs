using Store_App.Helpers;

namespace Store_App.Models.ProductModel
{
    public class ProductCreator : IProductCreator
    {
        public IProduct GetProduct()
        {
            return new Product(new DataContext());
        }

        public IProduct GetProduct(int productId, string? name, decimal price, decimal sale, decimal rating, string? manufacturer, string? description, string? category, decimal length, decimal width, decimal height, decimal weight, string? sku, string? imageLocation)
        {
            return new Product(productId, name, price, sale, rating, manufacturer, description, category, length, width, height, weight, sku, imageLocation, new DataContext());
        }
    }
}
