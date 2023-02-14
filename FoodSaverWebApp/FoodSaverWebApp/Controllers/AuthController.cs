using Firebase.Auth;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(AuthModel authModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Task<string> getToken = _auth.Registration(authModel);
                    
                    getToken.Wait();

                    string token = getToken.Result;

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (AggregateException ex)
                {
                    ex.Handle((x) =>
                    {
                        if (x is FirebaseAuthException)
                        {
                            var fbAuthEx = (FirebaseAuthException)x;
                            var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(fbAuthEx.ResponseData);
                            ModelState.AddModelError(String.Empty, _auth.AdjustErrorMessage(firebaseEx.error.message));

                            return true;
                        }
                        return false;
                    });

                    return View(authModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(authModel);
                }
            }
            else
            {
                return View(authModel);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthModel authModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Task<string> getToken = _auth.Login(authModel);
                    getToken.Wait();

                    string token = getToken.Result;

                    if (token != null)
                    {
                        HttpContext.Session.SetString("_UserToken", token);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (AggregateException ex)
                {
                    ex.Handle((x) =>
                    {
                        if (x is FirebaseAuthException)
                        {
                            var fbAuthEx = (FirebaseAuthException)x;
                            var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(fbAuthEx.ResponseData);
                            ModelState.AddModelError(String.Empty, _auth.AdjustErrorMessage(firebaseEx.error.message));

                            return true;
                        }
                        return false;
                    });

                    return View(authModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(authModel);
                }
            }
            else
            {
                return View(authModel);
            }
            
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Login");
        }
    }
}
