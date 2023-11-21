using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.Classes;


namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : Controller
    {
        [HttpGet("getonebasedonaccountid")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetOneBasedOnAccountId()
        {
            int? userAccountId = (int?)HttpContextHelper.GetUserId(HttpContext);
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
        [Authorize("ValidUser")]
        public ActionResult<string> AddToCart(int productId, int quantity)
        {
            int? accountId = (int?)HttpContextHelper.GetUserId(HttpContext);
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
        [Authorize("ValidUser")]
        public ActionResult<string> DeleteFromCart(int productId)
        {
            int? accountId = (int?)HttpContextHelper.GetUserId(HttpContext);
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

        [HttpGet("gettotalprice")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetTotalPrice()
        {
            int? userAccountId = (int?)HttpContextHelper.GetUserId(HttpContext);
            Cart cart = new Cart();
            double totalPrice = cart.GetTotalPrice(userAccountId);
            if (totalPrice >= 0)
            {
                return JsonConvert.SerializeObject(totalPrice);
            }
            else
            {
                Cart notFoundCart = new Cart
                {
                    Errors = new List<string> { "Error Getting Total Price" },
                    Success = false
                };
                return JsonConvert.SerializeObject(notFoundCart);
            }
        }

        [HttpGet("deleteallfromcart")]
        [Authorize("ValidUser")]
        public ActionResult<string> DeleteAllFromCart()
        {
            int? userAccountId = (int?)HttpContextHelper.GetUserId(HttpContext);
            Cart cart = new Cart();
            if (userAccountId != null)
            {
                cart.DeleteAllFromCart(userAccountId);
                return Ok(new { Success = true, Message = "Product deleted from cart successfully", Cart = cart });
            }
            else
            {
                Cart notFoundCart = new Cart
                {
                    Errors = new List<string> { "Error Deleting Products from Cart" },
                    Success = false
                };
                return JsonConvert.SerializeObject(notFoundCart);
            }
        }
    }
}
