using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class CartProduct : ICartProduct
    {
        public int CartProductId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Additional properties related to the relationship between the product and the cart
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties for relationships
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
