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
            Cart retrievedCart = Cart.GetOneBasedOnAccountId(userAccountId);
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
            Cart retrievedCart = Cart.GetOneBasedOnAccountId(accountId);
            if (retrievedCart != null)
            {
                //See if the cart already has the product
                CartProduct? foundProduct = retrievedCart.CartProducts.FirstOrDefault(cartProduct => cartProduct.ProductId == productId);
                if (foundProduct != null) //This means that we already have this in the cart
                {
                    CartProduct.UpdateCartProductQuantity(foundProduct.CartProductId, foundProduct.Quantity + quantity);
                }
                else
                {
                    Cart.AddToCart(retrievedCart, productId, quantity);
                }

                return Ok(new { Success = true, Message = "Product added to cart successfully" });

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
            Cart retrievedCart = Cart.GetOneBasedOnAccountId(accountId);
            if (retrievedCart != null)
            {
                Cart.DeleteFromCart(retrievedCart.CartId, productId);
                return Ok(new { Success = true, Message = "Product deleted from cart successfully"});

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
            double totalPrice = Math.Round(Cart.GetTotalPrice(userAccountId), 2);
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
            if (userAccountId != null)
            {
                Cart.DeleteAllFromCart(userAccountId);
                return Ok(new { Success = true, Message = "Product deleted from cart successfully"});
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
