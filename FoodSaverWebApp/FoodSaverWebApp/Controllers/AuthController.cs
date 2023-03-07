using FoodSaverWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
            
        }

        [HttpGet("/Register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("/Register")]
        public IActionResult Registration(RegisterModel authModel)
        {
            return View();
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            return View();
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
