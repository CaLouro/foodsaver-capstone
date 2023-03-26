using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;
using FoodSaverWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodSaverWebApp.Controllers
{
    public class AdminDashboardController : Controller
    {
        private IAuthService _authService;
        private IBusinessService _businessService;
        private IAddressService _addressService;
        private IItemService _itemService;
        private ITagService _tagService;
        private IDiscountInfoService _discountInfoService;
        private readonly ILogger<AdminDashboardController> _logger;

        public AdminDashboardController(IAuthService authService,
            IBusinessService businessService,
            IAddressService addressService,
            IItemService itemService,
            ITagService tagService,
            IDiscountInfoService discountInfoService,
            ILogger<AdminDashboardController> logger)
        {
            _authService = authService;
            _businessService = businessService;
            _addressService = addressService;
            _itemService = itemService;
            _tagService = tagService;
            _discountInfoService = discountInfoService;
            _logger = logger;
        }
        
        [HttpGet("/Admin")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns a list of businesses that the active user is an admin of to the desired view
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Admin/Stores")]
        public async Task<IActionResult> AdminStores()
        {
            User? user = await _authService.GetActiveUser();
            ICollection<Business>? businesses = null;
            
            if (user != null)
                businesses = await _businessService.GetAllBusinessesUnderAdmin(user);

            return View(businesses);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Register/Store")]
        public async Task<IActionResult> AdminRegisterStore()
        {
            ICollection<Tag> tags = await _tagService.GetAllTags();
            
            StoreModel storeModel = new StoreModel() 
            {
                Business = new Business
                {
                    Address = new Address()
                }
            };
            storeModel.ConfigureTagsToSelectList(tags);
            
            return View(storeModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeModel"></param>
        /// <returns></returns>
        [HttpPost("/Register/Store")]
        public async Task<IActionResult> AdminRegisterStore(StoreModel storeModel)
        {
            ModelState.Remove("ProvinceCodes");
            ModelState.Remove("TagItems");

            if (ModelState.IsValid)
            {
                Address? address = await _addressService.ReturnAddressOnInsert(storeModel.Business.Address);
                if (address != null)
                    storeModel.Business.AddressId = address.AddressId;

                ICollection<Tag> Tags = await _tagService.GetAllTags();
                List<int> formSelectedTags = storeModel.TagItems.Where(tag => tag.Selected)
                    .Select(tag => tag.Value)
                    .Select(int.Parse)
                    .ToList();

                storeModel.Business.Tags = Tags.Where(t => formSelectedTags.Any(t2 => t2 == t.TagId)).ToList();

                Business? business = await _businessService.ReturnBusinessOnInsert(storeModel.Business);
                if (business != null)
                    _businessService.LinkUserToBusiness(business);
                
                return RedirectToAction("AdminStores", "AdminDashboard");
            }

            return View(storeModel);
        }
        
        /// <summary>
        /// Sends a StoreModel that contains the selected business to be edited
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Edit")]
        public async Task<IActionResult> AdminEditStore(int businessId)
        {
            ICollection<Tag> tags = await _tagService.GetAllTags();
            
            StoreModel storeModel = new StoreModel() 
            {
                Business = await _businessService.GetBusiness(businessId)
            };
            storeModel.ConfigureTagsToSelectList(tags);

            return View(storeModel);
        }
        
        /// <summary>
        /// Verify that the information edited meets the models requirements and
        /// update the business and address appropriately
        /// </summary>
        /// <param name="storeModel"></param>
        /// <returns></returns>
        [HttpPost("/Admin/Store/{businessId}/Edit")]
        public async Task<IActionResult> AdminEditStore(StoreModel storeModel)
        {
            ModelState.Remove("ProvinceCodes");
            ModelState.Remove("Address");

            if (ModelState.IsValid)
            {
                await _addressService.UpdateAddress(storeModel.Business.Address);
                
                ICollection<Tag> Tags = await _tagService.GetAllTags();
                List<int> formSelectedTags = storeModel.TagItems.Where(tag => tag.Selected)
                    .Select(tag => tag.Value)
                    .Select(int.Parse)
                    .ToList();

                storeModel.Business.Tags = Tags.Where(t => formSelectedTags.Any(t2 => t2 == t.TagId)).ToList();
                
                await _businessService.UpdateBusiness(storeModel.Business);
                
                return RedirectToAction("AdminStores");
            }

            return View(storeModel);
        }

        /// <summary>
        /// Delete the business and everything associated with it
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Delete")]
        public async Task<IActionResult> AdminDeleteStore(int businessId)
        {
            await _businessService.DeleteBusiness(businessId);
            
            return RedirectToAction("AdminStores");
        }
        
        /// <summary>
        /// Get a list of items for the selected business
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Item/List")]
        public async Task<IActionResult> AdminStoreItems(int businessId)
        {
            ItemListModel itemListModel = new ItemListModel()
            {
                Items = await _itemService.GetAllItemsForBusiness(businessId),
                Business = await _businessService.GetBusiness(businessId)
            };

            return View(itemListModel);
        }

        /// <summary>
        /// Create a new item for the user to add information to
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Item/Add")]
        public IActionResult AdminAddItem(int businessId)
        {
            Item item = new Item()
            {
                BusinessId = businessId
            };
            
            return View(item);
        }
        
        /// <summary>
        /// Add item to database if the item meets the model requirements
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("/Admin/Store/{businessId}/Item/Add")]
        public async Task<IActionResult> AdminAddItem(Item item)
        {
            ModelState.Remove("Business");
            
            if (ModelState.IsValid)
            {
                await _itemService.InsertItem(item);
                
                return RedirectToAction("AdminStoreItems", new { businessId = item.BusinessId });
            }

            return View(item);
        }
        
        /// <summary>
        /// Send the Item entity that was selected to the view
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Item/{itemId}/Edit")]
        public async Task<IActionResult> AdminEditItem(int itemId)
        {
            Item? item = await _itemService.GetItem(itemId);
            
            return View(item);
        }
        
        /// <summary>
        /// Verifies that the edited info meets the model requirements and updates the item
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("/Admin/Store/{businessId}/Item/{itemId}/Edit")]
        public async Task<IActionResult> AdminEditItem(int businessId, Item item)
        {
            ModelState.Remove("Business");

            if (ModelState.IsValid)
            {
                await _itemService.UpdateItem(item);

                return RedirectToAction("AdminStoreItems", new { businessId = businessId });
            }
            
            return View(item);
        }
        
        /// <summary>
        /// Deletes the item
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("/Admin/Store/{businessId}/Item/{itemId}/Delete")]
        public async Task<IActionResult> AdminDeleteItem(int businessId, int itemId)
        {
            await _itemService.DeleteItem(itemId);
            
            return RedirectToAction("AdminStoreItems", new { businessId = businessId });
        }
    }
}
