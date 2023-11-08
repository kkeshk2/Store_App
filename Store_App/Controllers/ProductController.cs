using Microsoft.AspNetCore.Mvc;
using Store_App.Models.Classes;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        [HttpGet("/api/product/getone/{id}")]
        public Product GetOne(int id)
        {
            Product product = new Product();
            Product retrievedProduct = product.GetOne(id); // Replace with your logic
            if (retrievedProduct != null)
            {
                return retrievedProduct;
            }
            else
            {
                Product notFoundProduct = new Product
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                return notFoundProduct;
            }
        }

        [HttpGet("/api/product/getall")]
        public List<Product> GetAll()
        {
            Product product = new Product();
            List<Product> product_list = product.GetAll();
            if (product_list != null)
            {
                return product_list;
            }
            else
            {
                Product notFoundProduct = new Product
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                List<Product> product_errors_list = new List<Product>();
                product_errors_list.Add(notFoundProduct);

                return product_errors_list;
                ;
                ;
            }
        }

    }
}