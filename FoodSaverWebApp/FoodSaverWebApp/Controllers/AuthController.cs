using Firebase.Auth;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace FoodSaverWebApp.Controllers
{
    public class AuthController : Controller
    {
        private FirebaseAuthentication _auth;

        public AuthController()
        {
            _auth = new FirebaseAuthentication();
        }

        [HttpGet("/Register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("/Register")]
        public IActionResult Registration(RegisterModel authModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Request a token from Firebase
                    Task<string> getToken = _auth.Registration(authModel);
                    getToken.Wait();

                    string token = getToken.Result;

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                    }
                }
                catch (AggregateException ex) when (ex.InnerException is FirebaseAuthException fbAuthEx)
                {
                    // On a firebase exception, deserialize the response to be able to clearly display a message.
                    FirebaseError? firebaseError = JsonConvert.DeserializeObject<FirebaseError>(fbAuthEx.ResponseData);

                    // The firebase error message is run through the method "AdjustErrorMessage" to return a more
                    // descriptive response than what firebase returns.
                    ModelState.AddModelError(String.Empty, _auth.AdjustErrorMessage(firebaseError.error.message));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
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
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Task<string> getToken = _auth.Login(loginModel);
                    getToken.Wait();

                    string token = getToken.Result;

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                    }
                }
                catch (AggregateException ex) when (ex.InnerException is FirebaseAuthException fbAuthEx)
                {
                    // On a firebase exception, deserialize the response to be able to clearly display a message.
                    FirebaseError? firebaseError = JsonConvert.DeserializeObject<FirebaseError>(fbAuthEx.ResponseData);

                    // The firebase error message is run through the method "AdjustErrorMessage" to return a more
                    // descriptive response than what firebase returns.
                    ModelState.AddModelError(String.Empty, _auth.AdjustErrorMessage(firebaseError.error.message));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
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
