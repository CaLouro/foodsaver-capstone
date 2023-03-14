using System.Diagnostics;
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
            ICollection<Business> businesses = await _businessService.GetAllBusinesses();
            
            return View(businesses);
        }

        [HttpGet("/Dashboard/Store/{businessId}/Address")]
        public async Task<IActionResult> StoreAddress(int businessId)
        {
            Business? business = await _businessService.GetBusiness(businessId);

            return View(business);
        }

        [HttpGet("/Dashboard/Deals")]
        public async Task<IActionResult> Deals()
        {
            ICollection<Item> items = await _itemService.GetAllItems();

            return View(items);
        }
    }
}
