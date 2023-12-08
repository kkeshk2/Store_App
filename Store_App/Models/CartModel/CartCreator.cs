using Store_App.Helpers;

namespace Store_App.Models.CartModel
{
    public class CartCreator : ICartCreator
    {
        public ICart GetCart()
        {
            return new Cart(new CartProductCreator(), new DataContext());
        }
    }
}
