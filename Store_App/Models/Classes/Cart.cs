using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Cart:ICart
    {
        public int CartId { get; set; }
        public int AccountId { get; set; }
        
        // Define a list of CartProduct items
        public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
