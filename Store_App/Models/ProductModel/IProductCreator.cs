using Store_App.Helpers;

namespace Store_App.Models.ProductModel
{
    public interface IProductCreator
    {
        public IProduct GetProduct();
        public IProduct GetProduct(int productId, string? name, decimal price, decimal sale, decimal rating, string? manufacturer, string? description, string? category, decimal length, decimal width, decimal height, decimal weight, string? sku, string? imageLocation);
    }
}
