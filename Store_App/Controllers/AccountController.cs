using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store_App.Helpers;
using Store_App.Models.Classes;
using System.Net;
using Newtonsoft.Json;

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
        [HttpPost("createaccount")]
        public ActionResult<string> CreateAccount(string email, string password, string username)
        {
            try
            {
            Account new_account = Account.createAccount(email, password, username);
            return JsonConvert.SerializeObject(new_account);
            }
            catch (Exception)
            {
                Console.WriteLine("Account cannot be created");
            }
        }
                [HttpPut("updateaccount")]
        public ActionResult<string> UpdateAccount(string email, string username)
        {
            try
            {
                Account updated_account = Account.updateAccount(email, username);
                return JsonConvert.SerializeObject(updated_account);
            }
            catch (Exception)
            {
                Console.WriteLine("Account cannot be updated");
            }
        }
        [HttpPut("updateaccountpassword")]
        public ActionResult<string> UpdateAccountPassword(string password)
        {
            try
            {
                Account updated_password = Account.updateAccount(password);
                return JsonConvert.SerializeObject(updated_password);
            }
            catch (Exception)
            {
                Console.WriteLine("Password cannot be updated");
            }
        }
    }
}
