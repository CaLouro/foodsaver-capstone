using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using NuGet.Common;
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
                    string token = await _auth.CreateAccount(authModel);

                    return RedirectToAction("SetActiveAccount", new { token = token, displayName = authModel.Name });
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
                    string token = await _auth.SignIn(loginModel);

					return RedirectToAction("SetActiveAccount", new { token = token });
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
        public IActionResult Logout()
        {
            _auth.SignOut();
            HttpContext.Session.Remove("_UserToken");

			return RedirectToAction("Login");
        }

        /// <summary>
        /// Sets the session strings to reflect the current logged in account ie. active user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetActiveAccount(string token, string? displayName = null)
        {
            User? activeUser = await _auth.GetActiveUser();
            string? activeUserName = displayName;

            if (activeUser != null)
            {
                activeUserName = activeUser.DisplayName;
            }

            HttpContext.Session.SetString("_UserToken", token);
            HttpContext.Session.SetString("_DisplayName", activeUserName);

            return RedirectToAction("Index", "Home");
        }
    }
}
