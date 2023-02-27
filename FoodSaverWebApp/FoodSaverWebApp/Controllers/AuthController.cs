using Firebase.Auth;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class AuthController : Controller
    {
        private IFirebaseAuthentication _auth;

        public AuthController(IFirebaseAuthentication auth)
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
                    // Request a token from Firebase
                    string token = await _auth.Registration(authModel);

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    var firebaseError = _auth.ExtractFirebaseException(ex);

                    ModelState.AddModelError(firebaseError.error.modelError, firebaseError.error.message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong");
                }
            }

            if (HttpContext.Session.GetString("_UserToken") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(authModel);
            }
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
                    string token = await _auth.Login(loginModel);

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    var firebaseError = _auth.ExtractFirebaseException(ex);

                    ModelState.AddModelError(firebaseError.error.modelError, firebaseError.error.message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong");
                }
            }

            if (HttpContext.Session.GetString("_UserToken") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(loginModel);
            }
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Login");
        }
    }
}
