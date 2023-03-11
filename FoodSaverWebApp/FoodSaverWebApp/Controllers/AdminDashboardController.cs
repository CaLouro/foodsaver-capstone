using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class AdminDashboardController : Controller
    {
        [HttpGet("/Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
