using Store_App.Models.ProductModel;
using Store_App.Models.CartModel;
using Newtonsoft.Json;

namespace Store_App.Models.CartModel
{
    public class CartProduct : ICartProduct
    {
        [JsonProperty] private IProduct Product;
        [JsonProperty] private int Quantity;
        [JsonProperty] private decimal UnitPrice;

        public CartProduct(int productId, int quantity)
        {
            Product = new Product();
            Product.AccessProduct(productId);
            Quantity = quantity;
            UnitPrice = Product.GetProductPrice();
        }

        public CartProduct(int productId, int quantity, decimal price)
        {
            Product = new Product();
            Product.AccessProduct(productId);
            Quantity = quantity;
            UnitPrice = price;
        }

        public int GetProductId()
        {
            return Product.GetProductId();
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        public decimal GetUnitPrice()
        {
            return Product.GetProductPrice();
        }
    }
}
