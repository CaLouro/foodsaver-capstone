using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodSaverWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpGet("/Signup")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("/Signup")]
        public async Task<IActionResult> Registration(RegisterModel authModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _auth.CreateAccount(authModel);
            }
            catch (Supabase.Gotrue.BadRequestException ex)
            {
                var result = JsonConvert.DeserializeObject<RegisterError>(ex.Content);
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _auth.SignIn(loginModel);
            }
            catch (Supabase.Gotrue.BadRequestException ex)
            {
                var result = JsonConvert.DeserializeObject<LoginError>(ex.Content);
                ModelState.AddModelError(string.Empty, result.ErrorDescription);
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _auth.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}