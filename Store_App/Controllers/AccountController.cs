using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Store_App.Models.AccountModel;
using Store_App.Exceptions;

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
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
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
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IAccount account = new Account();
                account.AccessAccount(accountId);
                return JsonConvert.SerializeObject(account);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
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
        public ActionResult<string> CreateAccount(string email, string password)
        {
            try
            {
                IAccount account = new Account();
                account.CreateAccount(email, password);
                return account.GenerateToken();
            }
            catch (InvalidInputException)
            {
                return new StatusCodeResult(400);
            }
            catch (EmailTakenException)
            {
                return new StatusCodeResult(403);
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
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IAccount account = new Account();
                account.AccessAccount((int) accountId);
                account.UpdateEmail(email);
                return new StatusCodeResult(200);
            }
            catch (InvalidInputException)
            {
                return new StatusCodeResult(400);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
            }
            catch (EmailTakenException)
            {
                return new StatusCodeResult(403);
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
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IAccount account = new Account();
                account.AccessAccount(accountId);
                account.UpdatePassword(password);

                return new StatusCodeResult(200);
            }
            catch (InvalidInputException)
            {
                return new StatusCodeResult(400);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
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
