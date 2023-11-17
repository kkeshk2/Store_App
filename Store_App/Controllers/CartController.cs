using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

        [HttpGet("addtocart")]
        
        public ActionResult<string> AddToCart(int accountId, int productId, int quantity)
        {
            Cart cart = new Cart();
            Cart retrievedCart = cart.GetOneBasedOnAccountId(accountId);
            if (retrievedCart != null)
            {
                cart.AddToCart(retrievedCart, productId, quantity);
                return Ok(new { Success = true, Message = "Product added to cart successfully", Cart = cart });

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

        [HttpGet("deletefromcart")]
        public ActionResult<string> DeleteFromCart(int accountId, int productId)
        {
            Cart cart = new Cart();
            Cart retrievedCart = cart.GetOneBasedOnAccountId(accountId);
            if (retrievedCart != null)
            {
                cart.DeleteFromCart(retrievedCart.CartId, productId);
                return Ok(new { Success = true, Message = "Product deleted from cart successfully", Cart = cart });

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
