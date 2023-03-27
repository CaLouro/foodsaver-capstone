using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly IItemService _itemService;
        private readonly IDiscountInfoService _discountService;

        public DashboardController(IBusinessService businessService,
            IItemService itemService,
            IDiscountInfoService discountService)
        {
            _businessService = businessService;
            _itemService = itemService;
            _discountService = discountService;
        }

        [HttpGet("/Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Dashboard/Stores")]
        public async Task<IActionResult> Stores()
        {
            var business = await _businessService.GetAllBusinesses();
            var userBusinesses = await _businessService.GetUserFavoriteBusinesses();

            var model = new StoreDashboardModel
            {
                Businesses = business.ToList(),
                FavoriteBusinesses = userBusinesses.ToList(),
            };

            return View(model);
        }

        [HttpGet("/Dashboard/Store/{businessId}")]
        public async Task<IActionResult> StoreListings(int businessId)
        {
            ItemListModel itemListModel = new ItemListModel()
            {
                Items = await _itemService.GetAllItemsForBusiness(businessId),
                Business = await _businessService.GetBusiness(businessId)
            };

            return View(itemListModel);
        }

        [HttpGet("/Dashboard/Deals")]
        public async Task<IActionResult> Deals()
        {
            var deals = await _discountService.GetAllDiscountInfos();

            return View(deals);
        }

        [HttpGet("/Dashboard/Favorites")]
        public async Task<IActionResult> Favorites()
        {
            var business = await _businessService.GetAllBusinesses();
            var userBusinesses = await _businessService.GetUserFavoriteBusinesses();

            var model = new StoreDashboardModel
            {
                Businesses = business.ToList(),
                FavoriteBusinesses = userBusinesses.ToList(),
            };

            return View(model);
        }

        [HttpPost("/Dashboard/ToggleFavorite")]
        public async Task<IActionResult> BusinessFavoriteToggle(int? businessId, bool? isFavorite)
        {
            if (businessId == null || isFavorite == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            await _businessService.ToggleBusinessFavorite((int)businessId, (bool)isFavorite);
            return new OkResult();
        }
    }
}