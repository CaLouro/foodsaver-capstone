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
        
        [HttpGet("/Dashboard/RegisterStore")]
        public IActionResult RegisterStore()
        {
            StoreModel storeModel = new StoreModel()
            {
                Business = new Business()
            };

            storeModel.Business.Address = new Address();
            
            return View(storeModel);
        }
        
        [HttpPost("/Dashboard/RegisterStore")]
        public async Task<IActionResult> RegisterStore(StoreModel storeModel)
        {
            ModelState.Remove("ProvinceCodes");

            if (ModelState.IsValid)
            {
                Address? address = await _addressService.ReturnAddressOnInsert(storeModel.Business.Address);

                if (address != null)
                    storeModel.Business.AddressId = address.AddressId;

                Business? business = await _businessService.ReturnBusinessOnInsert(storeModel.Business);

                if (business != null)
                    _businessService.LinkUserToBusiness(business);
                
                return RedirectToAction("AdminStores", "AdminDashboard");
            }
            
            storeModel.ProvinceCodes = new List<string>()
            {
                "NL",
                "PE",
                "NS",
                "NB",
                "QC",
                "ON",
                "MB",
                "NL",
                "AB",
                "BC",
                "YT",
                "NT",
                "NU"
            };

            return View(storeModel);
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
