using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface ICart
    {
        Cart GetOneBasedOnAccountId(int userAccountID);
        void AddToCart(Cart cart, int productId, int quantity);
        void DeleteFromCart(int cartId, int productId);
    }
}
