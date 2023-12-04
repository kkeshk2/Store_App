using Store_App.Models.ProductModel;
using Store_App.Models.CartModel;
using Newtonsoft.Json;

namespace Store_App.Models.CartModel
{
    public class CartProduct : ICartProduct
    {
        [JsonProperty] private IProduct Product;
        [JsonProperty] private decimal Price;
        [JsonProperty] private int Quantity;
        

        public CartProduct(int productId, int quantity)
        {
            Product = new Product();
            Product.AccessProduct(productId);
            Price = Product.GetPrice();
            Quantity = quantity;       
        }

        public CartProduct(int productId, int quantity, decimal price)
        {
            Product = new Product();
            Product.AccessProduct(productId);
            Quantity = quantity;
            Price = price;
        }

        public int GetProductId()
        {
            return Product.GetProductId();
        }

        public decimal GetPrice()
        {
            return Price;
        }

        public int GetQuantity()
        {
            return Quantity;
        }        
    }
}
