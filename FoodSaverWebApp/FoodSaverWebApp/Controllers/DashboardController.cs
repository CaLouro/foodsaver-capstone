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
        public DashboardController(IBusinessService businessService, IAddressService addressService)
        {
            _businessService = businessService;
            _addressService = addressService;
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
            StoreRegistrationModel storeRegistrationModel = new StoreRegistrationModel()
            {
                Business = new Business(),
                Address = new Address(),
                ProvinceCodes = new List<string>()
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
                }
            };
            
            return View(storeRegistrationModel);
        }
        
        [HttpPost("/Dashboard/RegisterStore")]
        public async Task<IActionResult> RegisterStore(StoreRegistrationModel storeRegistrationModel)
        {
            ModelState.Remove("ProvinceCodes");
            ModelState.Remove("Business.Address");

            if (ModelState.IsValid)
            {
                //Address? address = await _addressService.ReturnAddressOnInsert(storeRegistrationModel.Address);
                Address? address = await _addressService.GetAddress(12);

                if (address != null)
                    storeRegistrationModel.Business.Address = address;

                Business? business = await _businessService.ReturnBusinessOnInsert(storeRegistrationModel.Business);

                if (business != null)
                    _businessService.LinkUserToBusiness(business);
                
                return RedirectToAction("Stores");
            }
            
            storeRegistrationModel.ProvinceCodes = new List<string>()
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

            return View(storeRegistrationModel);
        }

        [HttpGet("/Dashboard/Store/{businessId}/Address")]
        public async Task<IActionResult> StoreAddress(int businessId)
        {
            Business? business = await _businessService.GetBusiness(businessId);

            return View(business);
        }

        [HttpGet("/Dashboard/Deals")]
        public IActionResult Deals()
        {
            return View();
        }
    }
}
