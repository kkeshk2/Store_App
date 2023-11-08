using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store_App.Models.Classes;


namespace Store_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        [HttpGet("/api/cart/getonebasedonaccountid")]
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
    }
}
