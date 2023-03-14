using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class ItemService : IItemService
    {
        private readonly IDbConnection _connection;
        public ItemService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<ICollection<Item>> GetAllItems()
        {
            var result = await _connection.AccessDatabase()
                .From<Item>()
                .Get();

            return result.Models;
        }

        public async Task<ICollection<Item>> GetAllItemsForBusiness(int businessId)
        {
            var result = await _connection.AccessDatabase()
                .From<Item>()
                .Where(i => i.BusinessId == businessId)
                .Get();

            return result.Models;
        }

        public async Task<Item?> GetItem(int itemId)
        {
            Item? result = await _connection.AccessDatabase()
                .From<Item>()
                .Where(x => x.ItemId == itemId)
                .Single();

            return result;
        }

        public async Task InsertItem(Item item)
        {
            await _connection.AccessDatabase()
                .From<Item>()
                .Insert(item);
        }

        public async Task UpdateItem(Item item)
        {
            await _connection.AccessDatabase()
                .From<Item>()
                .Update(item);
        }

        public async Task DeleteItem(int itemId)
        {
            await _connection.AccessDatabase()
                .From<Item>()
                .Where(x => x.ItemId == itemId)
                .Delete();
        }
    }
}