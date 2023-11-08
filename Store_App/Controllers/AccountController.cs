using Microsoft.AspNetCore.Mvc;
using Store_App.Models.Classes;


namespace Store_App.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
