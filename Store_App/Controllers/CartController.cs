using Microsoft.AspNetCore.Mvc;
using Store_App.Models.Classes;


namespace Store_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        [HttpGet("/api/cart/getonebasedonaccountid/{id}")]
        public Cart GetOneBasedOnAccountId(int userAccountId)
        {
            Cart cart = new Cart();
            Cart retrievedCart = cart.GetOneBasedOnAccountId(userAccountId);
            if (retrievedCart != null)
            {
                return retrievedCart;
            }
            else
            {
                Cart notFoundCart = new Cart
                {
                    Errors = new List<string> { "Product not found" },
                    Success = false
                };
                return notFoundCart;
            }
        }
    }
}
