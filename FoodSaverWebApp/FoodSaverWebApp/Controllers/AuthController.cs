using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodSaverWebApp.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpGet("/Register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Registration(RegisterModel authModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _auth.CreateAccount(authModel);

                    return RedirectToAction("Index", "Home");
                }
                catch (Supabase.Gotrue.BadRequestException ex)
                {
                    RegisterError? result = JsonSerializer.Deserialize<RegisterError>(ex.Content);
                    ModelState.AddModelError(string.Empty, result.msg);
                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _auth.SignIn(loginModel);

                    return RedirectToAction("Index", "Home");
                }
                catch (Supabase.Gotrue.BadRequestException ex)
                {
                    LoginError? result = JsonSerializer.Deserialize<LoginError>(ex.Content);
                    ModelState.AddModelError(string.Empty, result.error_description);
                    return View();
                }
            }

            return View();
        }

        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _auth.SignOut();

            return RedirectToAction("Login");
        }
    }
}