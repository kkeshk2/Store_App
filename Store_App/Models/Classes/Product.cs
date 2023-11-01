using Store_App.Models.Interfaces;
using System.Collections.Generic;

namespace Store_App.Models.Classes
{
    public class Product : IProduct
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

        public IEnumerable<Product> GetAll()
        {
            // Add logic to retrieve all products from your data source
            // For example, you can query a database or another data store
            // Create and return a collection of Product instances
            // If there are errors or the retrieval fails, set Errors and Success accordingly
            return new List<Product>(); // Placeholder return
        }

        public Product GetOne(int id)
        {
            Product product = new Product();
            product.ProductId = id;
            Success = true;
            return product;
            // Add logic to retrieve a product by its ProductId
            // Create and return a Product instance with the retrieved data
            // If the product is not found, set Success to false and add an error message
            // If there are errors or the retrieval fails, set Errors and Success accordingly
           // return new Product(); // Placeholder return
        }

        public Product Save(Product model)
        {
            // Add logic to save a new product
            // If save is successful, return true and set Success to true
            // If there are errors or the save fails, set Errors and Success accordingly
            return new Product();// Placeholder return
        }

        public Product Update(Product model)
        {
            // Add logic to update an existing product
            // If update is successful, return true and set Success to true
            // If there are errors or the update fails, set Errors and Success accordingly
            return new Product(); ; // Placeholder return
        }
    }
}
