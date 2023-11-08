using Microsoft.AspNetCore.Mvc;
using Store_App.Models.Classes;
using Newtonsoft.Json;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {

        [HttpGet("getone")]
        public ActionResult<string> GetOne(int prodID)
        {
            Product product = new Product();
            Product retrievedProduct = product.GetOne(prodID); // Replace with your logic
            if (retrievedProduct != null)
            {
                return JsonConvert.SerializeObject(retrievedProduct);
            }
            else
            {
                Product notFoundProduct = new Product
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                return JsonConvert.SerializeObject(notFoundProduct);
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Product product = new Product();
            List<Product> productList = product.GetAll(); 

            if (productList != null)
            {
                return Ok(productList); // Return the list of products as JSON
            }
            else
            {
                Product notFoundProduct = new Product
                {
                    Errors = new List<string> { "Products not found" },
                    Success = false
                };
                return NotFound(notFoundProduct); // Return a 404 Not Found response with the error message
            }
        }


    }
}