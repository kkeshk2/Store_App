using Store_App.Models.ProductModel;

namespace Store_App.Models.CartModel
{
    public interface ICart
    {
        public void AccessCart(int accountId);
        public void AddToCart(int productId, int quantity);
        public int Contains(int productId);
        public void ClearCart();
        public void DeleteItem(int accountId);
        public List<ICartProduct> GetProductList();
        public void UpdateCart(int productId, int quantity);
    }
}
