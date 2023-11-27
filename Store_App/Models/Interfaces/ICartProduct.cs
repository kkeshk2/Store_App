using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface ICartProduct
    {
        // Interface method or property declarations go here
        CartProduct GetOne(int cartId, int productId);
        List<CartProduct> GetCartProductsBasedOnCart(int cartProductId);
        void AddOneToCartProductDatabase(int cartId, int productId, int quantity);
        void DeleteFromCartProductDatabase(int cartProductId);
        void DeleteCartProductsForOneCart(int cartId);
    }
}
