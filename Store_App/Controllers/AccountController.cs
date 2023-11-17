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
    }
}
