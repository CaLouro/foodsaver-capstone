using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IBusinessService _businessService;
        private readonly IItemService _itemService;
        private readonly IDiscountInfoService _discountService;
        private readonly ISearchService _searchService;

        public DashboardController(IBusinessService businessService,
            IItemService itemService,
            IDiscountInfoService discountService,
            ISearchService searchService,
            ILogger<DashboardController> logger)
        {
            _businessService = businessService;
            _itemService = itemService;
            _discountService = discountService;
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet("/Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Dashboard/Stores")]
        public async Task<IActionResult> Stores([FromQuery(Name = "search")] string? search)
        {
            var business = await _businessService.GetAllBusinesses();
            var userBusinesses = await _businessService.GetUserFavoriteBusinesses();

            var model = new StoreDashboardModel
            {
                Businesses = business.ToList(),
                FavoriteBusinesses = userBusinesses.ToList(),
            };

            if (!string.IsNullOrWhiteSpace(search))
            {
                _logger.LogInformation($"Business search query: {search}");
                model.Businesses = await _searchService.Search(model.Businesses, search);
            }

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
        public async Task<IActionResult> Deals([FromQuery(Name = "search")] string? search)
        {
            var deals = await _discountService.GetAllDiscountInfos();

            if (!string.IsNullOrWhiteSpace(search))
            {
                _logger.LogInformation($"Deals search query: {search}");
                deals = await _searchService.Search(deals.ToList(), search);
            }

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