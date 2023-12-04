using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store_App.Exceptions;
using Store_App.Models.ProductModel;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {

        [HttpGet("accessproduct")]
        public ActionResult<string> AccessProduct(int productId)
        {
            try
            {
                IProduct product = new Product();
                product.AccessProduct(productId);
                return JsonConvert.SerializeObject(product);
            }
            catch (ProductNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("accessproductlist")]
        public ActionResult<string> GetAll()
        {
            try
            {
                IProductList productList = new ProductList();
                productList.AccessProductList();
                return JsonConvert.SerializeObject(productList);
            }
            catch (ProductNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}