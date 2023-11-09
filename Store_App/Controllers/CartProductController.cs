using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store_App.Models.Classes;

namespace Store_App.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class CartProductController
    {
        [HttpGet("getone")]
        public ActionResult<string> GetOne(int cartId, int productId)
        {
            CartProduct cartProd = new CartProduct();
            CartProduct retrievedCartProd = cartProd.GetOne(cartId, productId);
            if (retrievedCartProd != null)
            {
                return JsonConvert.SerializeObject(retrievedCartProd);
            }
            else
            {
                CartProduct notFoundCartProd = new CartProduct
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                return JsonConvert.SerializeObject(notFoundCartProd);
            }
        }
    }
}
