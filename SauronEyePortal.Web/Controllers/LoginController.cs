using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SauronEyePortal.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Autenticate(string email,string password)
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
