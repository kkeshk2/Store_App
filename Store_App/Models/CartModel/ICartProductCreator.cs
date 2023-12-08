namespace Store_App.Models.CartModel
{
    public interface ICartProductCreator
    {
        public ICartProduct GetCartProduct(int productId, int quantity);
        public ICartProduct GetCartProduct(int productId, int quantity, decimal price);
    }
}
