using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSaverWebApp.Controllers
{
    public class AdminDashboardController : Controller
    {
        private IAuthService _authService;
        private IBusinessService _businessService;
        private IAddressService _addressService;
        private IItemService _itemService;
        private IDiscountInfoService _discountInfoService;

        public AdminDashboardController(IAuthService authService,
            IBusinessService businessService,
            IAddressService addressService,
            IItemService itemService,
            IDiscountInfoService discountInfoService)
        {
            _authService = authService;
            _businessService = businessService;
            _addressService = addressService;
            _itemService = itemService;
            _discountInfoService = discountInfoService;
        }
        
        [HttpGet("/Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Admin/Stores")]
        public async Task<IActionResult> AdminStores()
        {
            User? user = await _authService.GetActiveUser();
            ICollection<Business>? businesses = null;
            
            if (user != null)
                businesses = await _businessService.GetAllBusinessesUnderAdmin(user);

            return View(businesses);
        }
        
        [HttpGet("/Admin/Store/{businessId}/Edit")]
        public async Task<IActionResult> AdminEditStore(int businessId)
        {
            StoreModel storeModel = new StoreModel();
            storeModel.Business = await _businessService.GetBusiness(businessId);

            return View(storeModel);
        }
        
        [HttpPost("/Admin/Store/{businessId}/Edit")]
        public async Task<IActionResult> AdminEditStore(StoreModel storeModel)
        {
            ModelState.Remove("ProvinceCodes");
            ModelState.Remove("Address");

            if (ModelState.IsValid)
            {
                await _addressService.UpdateAddress(storeModel.Business.Address);
                await _businessService.UpdateBusiness(storeModel.Business);
                
                return RedirectToAction("AdminStores");
            }

            return View(storeModel);
        }

        [HttpGet("/Admin/Store/{businessId}/Delete")]
        public async Task<IActionResult> AdminDeleteStore(int businessId)
        {
            await _businessService.DeleteBusiness(businessId);
            
            return RedirectToAction("AdminStores");
        }
    }
}
