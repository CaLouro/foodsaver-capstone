using System.Diagnostics;
using FoodSaverWebApp.Entities;
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
            ICollection<Business> businesses = await _businessService.GetAllBusinesses();
            
            return View(businesses);
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
    }
}
