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
            try
            {
            Account new_account = Account.createAccount(email, password, username);
            return JsonConvert.SerializeObject(new_account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
                [HttpPut("updateaccount")]
        public ActionResult<string> UpdateAccount(string email, string username)
        {
            try
            {
                Account account = new Account();
                account.updateAccount(email, username);  
                return Ok("Account updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating account: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("updateaccountpassword")]
        public ActionResult<string> UpdateAccountPassword(string password)
        {
            try
            {
                Account updated_password = new Account();
                updated_password.updateAccountPassword(password);
                return Ok("Account updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating account: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
