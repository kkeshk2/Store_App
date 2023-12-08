using Store_App.Models.ProductModel;

namespace Store_App.Models.CartModel
{
    public class CartProductCreator : ICartProductCreator
    {
        public ICartProduct GetCartProduct(int id, int quantity)
        {
            return new CartProduct(id, quantity, new ProductCreator());
        }

        public ICartProduct GetCartProduct(int id, int quantity, decimal price)
        {
            return new CartProduct(id, quantity, price, new ProductCreator());
        }
    }
}
