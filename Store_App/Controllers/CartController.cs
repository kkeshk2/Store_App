using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using Newtonsoft.Json;
using Store_App.Models.CartModel;
using Store_App.Exceptions;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : Controller
    {
        [HttpGet("accesscart")]
        [Authorize("ValidUser")]
        public ActionResult<string> AccessCart()
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                ICart cart = new Cart();
                cart.AccessCart(accountId);
                return JsonConvert.SerializeObject(cart);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
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

        [HttpGet("addtocart")]
        [Authorize("ValidUser")]
        public ActionResult AddToCart(int productId, int quantity)
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                ICart cart = new Cart();
                cart.AccessCart(accountId);
                cart.AddToCart(productId, quantity);
                return new OkResult();
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
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

        [HttpGet("deletefromcart")]
        [Authorize("ValidUser")]
        public ActionResult DeleteFromCart(int productId)
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                ICart cart = new Cart();
                cart.AccessCart(accountId);
                cart.DeleteItem(productId);
                return new OkResult();
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
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

        [HttpGet("clearcart")]
        [Authorize("ValidUser")]
        public ActionResult ClearCart()
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                ICart cart = new Cart();
                cart.AccessCart(accountId);
                cart.ClearCart();
                return new OkResult();
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
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

        [HttpGet("updatecart")]
        [Authorize("ValidUser")]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                ICart cart = new Cart();
                cart.AccessCart(accountId);
                cart.UpdateCart(productId, quantity);
                return new OkResult();
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
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
