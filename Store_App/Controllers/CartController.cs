using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store_App.Models.Classes;


namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : Controller
    {
        [HttpGet("getonebasedonaccountid")]
        public ActionResult<string> GetOneBasedOnAccountId(int userAccountId)
        {
            Cart cart = new Cart();
            Cart retrievedCart = cart.GetOneBasedOnAccountId(userAccountId);
            if (retrievedCart != null)
            {
                return JsonConvert.SerializeObject(retrievedCart);
            }
            else
            {
                Cart notFoundCart = new Cart
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                return JsonConvert.SerializeObject(notFoundCart);
            }
        }

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
