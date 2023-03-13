using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface IBusinessService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for Business entities
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Business>> GetAllBusinesses();
        
        /// <summary>
        /// Retrieves a single Business entity given the businessId
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public Task<Business?> GetBusiness(int businessId);
        
        /// <summary>
        /// Insert the given Business entity into the database
        /// </summary>
        /// <param name="business"></param>
        public void InsertBusiness(Business business);
        
        /// <summary>
        /// Returns the business entity that was just inserted. Useful for getting the incremented id
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<Business?> ReturnBusinessOnInsert(Business business);
        
        /// <summary>
        /// Update an existing Business entity inside of the database
        /// </summary>
        /// <param name="business"></param>
        public void UpdateBusiness(Business business);
        
        /// <summary>
        /// Delete a Business entity from the database given the businessId
        /// </summary>
        /// <param name="businessId"></param>
        public void DeleteBusiness(int businessId);

        /// <summary>
        /// Links the active session user to the created business
        /// </summary>
        /// <param name="business"></param>
        public void LinkUserToBusiness(Business business);
    }
}