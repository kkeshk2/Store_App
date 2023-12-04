using Store_App.Models.ProductModel;

namespace Store_App.Models.CartModel
{
    public interface ICartProduct
    {
        public int GetProductId();
        public decimal GetPrice();
        public int GetQuantity();       
    }
}
