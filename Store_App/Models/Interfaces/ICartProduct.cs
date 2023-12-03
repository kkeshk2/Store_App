using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface ICartProduct
    {
        // Interface method or property declarations go here
        public static abstract CartProduct GetOne(int cartId, int productId);
        public static abstract List<CartProduct> GetCartProductsBasedOnCart(int cartProductId);
        public static abstract void AddOneToCartProductDatabase(int cartId, int productId, int quantity);
        public static abstract void DeleteFromCartProductDatabase(int cartProductId);
        public static abstract void DeleteCartProductsForOneCart(int cartId);
        public static abstract void UpdateCartProductQuantity(int cartProductId, int newQuantity);

    }
}
