using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface IBusinessService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for the entity Business
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
        /// Update an existing Business entity inside of the database
        /// </summary>
        /// <param name="business"></param>
        public void UpdateBusiness(Business business);
        
        /// <summary>
        /// Delete a Business entity from the database given the businessId
        /// </summary>
        /// <param name="businessId"></param>
        public void DeleteBusiness(int businessId);
    }
}