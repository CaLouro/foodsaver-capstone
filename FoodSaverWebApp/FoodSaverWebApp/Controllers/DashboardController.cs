using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private IBusinessService _businessService;
        private IAddressService _addressService;
        private IItemService _itemService;

        public DashboardController(IBusinessService businessService,
            IAddressService addressService,
            IItemService itemService)
        {
            _businessService = businessService;
            _addressService = addressService;
            _itemService = itemService;
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
            ICollection<Item> items = await _itemService.GetAllItems();

            return View(items);
        }

        [HttpPost("/Dashboard/ToggleFavorite")]
        public async Task<IActionResult> BusinessFavoriteToggle(int? businessId, bool? isFavorite)
        {
            if (businessId != null && isFavorite != null)
            {
                await _businessService.ToggleBusinessFavorite((int)businessId, (bool)isFavorite);
            }

            return RedirectToAction("Index");
        }
    }
}