using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface IItemService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for Item entities
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Item>> GetAllItems();
        
        /// <summary>
        /// Retrieves a single Item entity given the itemId
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Task<Item?> GetItem(int itemId);
        
        /// <summary>
        /// Insert the given Item entity into the database
        /// </summary>
        /// <param name="item"></param>
        public Task InsertItem(Item item);
        
        /// <summary>
        /// Update an existing Item entity inside of the database
        /// </summary>
        /// <param name="item"></param>
        public Task UpdateItem(Item item);
        
        /// <summary>
        /// Delete a Item entity from the database given the itemId
        /// </summary>
        /// <param name="itemId"></param>
        public Task DeleteItem(int itemId);
    }
}