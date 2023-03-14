using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface IDiscountInfoService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for DiscountInfo entities
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<DiscountInfo>> GetAllDiscountInfos();
        
        /// <summary>
        /// Retrieves a single DiscountInfo entity given the discountInfoId
        /// </summary>
        /// <param name="discountInfoId"></param>
        /// <returns></returns>
        public Task<DiscountInfo?> GetDiscountInfo(int discountInfoId);
        
        /// <summary>
        /// Insert the given DiscountInfo entity into the database
        /// </summary>
        /// <param name="discountInfo"></param>
        public Task InsertDiscountInfo(DiscountInfo discountInfo);
        
        /// <summary>
        /// Update an existing DiscountInfo entity inside of the database
        /// </summary>
        /// <param name="discountInfo"></param>
        public Task UpdateDiscountInfo(DiscountInfo discountInfo);
        
        /// <summary>
        /// Delete a DiscountInfo entity from the database given the discountInfoId
        /// </summary>
        /// <param name="discountInfoId"></param>
        public Task DeleteDiscountInfo(int discountInfoId);
    }
}