using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Product:IProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductManufacturer { get; set; }
        public decimal ProductRating { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductLength { get; set; }
        public decimal ProductWidth { get; set; }
        public decimal ProductHeight { get; set; }
        public decimal ProductWeight { get; set; }
        public string ProductSKU { get; set; }
          // Error messages array
        public List<string> Errors { get; set; } = new List<string>();
        
        // Success boolean field
        public bool Success { get; set; } = true;
    }
}
