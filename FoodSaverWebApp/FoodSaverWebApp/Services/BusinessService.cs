using System.Data;
using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class BusinessService : DbConnection, IBusinessService
    {
        public BusinessService() { InitializeDatabaseConnection(); }
        
        public async Task<ICollection<Business>> GetAllBusinesses()
        {
            var result = await _database
                .From<Business>()
                .Get();

            return result.Models;
        }

        public async Task<Business> GetBusiness(int businessId)
        {
            Business? result = await _database
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Single();

            return result;
        }

        public async void InsertBusiness(Business business)
        {
            await _database
                .From<Business>()
                .Insert(business);
        }

        public async void UpdateBusiness(Business business)
        {
            await _database
                .From<Business>()
                .Update(business);
        }

        public async void DeleteBusiness(int businessId)
        {
            await _database
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Delete();
        }
    }
}