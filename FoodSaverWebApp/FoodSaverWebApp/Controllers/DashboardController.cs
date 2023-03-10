using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet("/Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Dashboard/Stores")]
        public IActionResult Stores()
        {
            return View();
        }

        [HttpGet("/Dashboard/Deals")]
        public IActionResult Deals()
        {
            return View();
        }
    }
}
