using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using Store_App.Models.Classes;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

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
                Account account = Account.accessAccountByLogin(email, password);
                return JWTHelper.GetToken(account.AccountId);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(502);
            }
        }


        [HttpGet("verify")]
        [Authorize("ValidUser")]
        public ActionResult<string> Verify()
        {
            return new OkResult();
        }

        [HttpGet("createaccount")]
        public ActionResult<string> CreateAccount(string email, string password, string name)
        {
            if (!Regex.IsMatch(email, "^[^@\\s@]+@[^@\\s]+\\.[^@\\s]+$")) return new StatusCodeResult(400);
            if (!Regex.IsMatch(password, "^[^\\s]{8,128}$")) return new StatusCodeResult(400);
            if (!Regex.IsMatch(name, "^[A-Za-z][A-Za-z\\.\\-\\x20]{0,127}$")) return new StatusCodeResult(400);
            try
            {
                int count = Account.accessAccountByEmail(email);
                if (count != 0) return new StatusCodeResult(401);
                Account account = Account.createAccount(email, password, name);
                return JWTHelper.GetToken(account.AccountId);
            }
            catch (Exception)
            {
                return new StatusCodeResult(502);
            }
        }

        [HttpPut("updateaccount")]
        [Authorize("ValidUser")]
        public ActionResult<string> UpdateAccount(string email, string username)
        {
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);
                Account account = Account.accessAccountById((int) userId);
                account.updateAccount(email, username);
                return new StatusCodeResult(200);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(502);
            }
        }

        [HttpPut("updateaccountpassword")]
        [Authorize("ValidUser")]
        public ActionResult<string> UpdateAccountPassword(string password)
        {
            try
            {
                int? userId = HttpContextHelper.GetUserId(HttpContext);
                if (userId == null) return new StatusCodeResult(401);
                Account account = Account.accessAccountById((int)userId);
                account.updateAccountPassword(password);
                return new StatusCodeResult(200);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(401);
            }
            catch (Exception)
            {
                return new StatusCodeResult(502);
            }
        }
    }
}
