using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface ICartProduct
    {
        // Interface method or property declarations go here
        List<CartProduct> GetCartProductsBasedOnCart(int cartProductId);
    }
}
