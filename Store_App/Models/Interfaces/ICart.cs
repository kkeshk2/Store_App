using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface ICart
    {
        public static abstract Cart GetOneBasedOnAccountId(int? userAccountId);
        public static abstract void AddToCart(Cart cart, int productId, int quantity);
        public static abstract void DeleteFromCart(int cartId, int productId);
        public static abstract double GetTotalPrice(int? accountId);
        public static abstract void DeleteAllFromCart(int? accountId);
    }
}
