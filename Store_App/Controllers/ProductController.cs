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

    }
}