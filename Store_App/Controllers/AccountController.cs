using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Store_App.Models.AccountModel;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("login")]
        public ActionResult<string> LogIn(string email, string password)
        {            
            try
            {
                IAccount account = new Account();
                account.AccessAccount(email, password);
                return account.GenerateToken();
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("accessaccount")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetAccount()
        {
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);

                IAccount account = new Account();
                account.AccessAccount((int) userId);
                return JsonConvert.SerializeObject(account);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("verifyaccount")]
        [Authorize("ValidUser")]
        public ActionResult<string> Verify()
        {
            return new OkResult();
        }

        [HttpGet("createaccount")]
        public ActionResult<string> CreateAccount(string email, string password, string name)
        {
            try
            {
                IAccount account = new Account();
                account.CreateAccount(email, password, name);
                return account.GenerateToken();
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(400);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("updateaccountemail")]
        [Authorize("ValidUser")]
        public ActionResult<string> UpdateAccountEmail(string email)
        {           
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);

                IAccount account = new Account();
                account.AccessAccount((int) userId);
                account.UpdateAccountEmail(email);
                return new StatusCodeResult(200);
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(400);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("updateaccountname")]
        [Authorize("ValidUser")]
        public ActionResult<string> UpdateAccountName(string name)
        {         
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);

                IAccount account = new Account();
                account.AccessAccount((int) userId);
                account.UpdateAccountName(name);

                return new StatusCodeResult(200);
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(400);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("updateaccountpassword")]
        [Authorize("ValidUser")]
        public ActionResult<string> UpdateAccountPassword(string password)
        {
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);

                IAccount account = new Account();
                account.AccessAccount((int)userId);
                account.UpdateAccountPassword(password);

                return new StatusCodeResult(200);
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(400);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
