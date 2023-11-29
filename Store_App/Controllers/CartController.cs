using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using System.Text.Json;
using Newtonsoft.Json;
using Store_App.Models.CartModel;
using Microsoft.Identity.Client;
using Store_App.Models.ProductModel;

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
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);

                return JsonConvert.SerializeObject(cart);
            }
            catch (InvalidOperationException)
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
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                cart.AddToCart(productId, quantity);
                return new OkResult();
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("contains")]
        [Authorize("ValidUser")]
        public ActionResult<string> Contains(int productId)
        {
            try
            {
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                var response = cart.Contains(productId);
                return JsonConvert.SerializeObject(response);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
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
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                cart.DeleteItem(productId);
                return new OkResult();
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
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
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                cart.ClearCart();
                return new OkResult();
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
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
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                cart.UpdateCart(productId, quantity);
                return new OkResult();
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("gettotal")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetCartTotal()
        {
            try
            {
                int? accountId = HttpContextHelper.GetUserId(HttpContext);
                if (accountId == null) return new StatusCodeResult(401);

                ICart cart = new Cart();
                cart.AccessCart((int)accountId);
                var result = cart.GetProductList().Sum(p => p.GetUnitPrice() * p.GetQuantity());
                return new OkObjectResult(result);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
