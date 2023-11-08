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
                return "";
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