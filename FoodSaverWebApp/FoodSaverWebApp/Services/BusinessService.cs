using System.Data;
using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IDbConnection _connection;
        public BusinessService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<ICollection<Business>> GetAllBusinesses()
        {
            var result = await _connection.AccessDatabase()
                .From<Business>()
                .Get();

            return result.Models;
        }

        public async Task<Business?> GetBusiness(int businessId)
        {
            Business? result = await _connection.AccessDatabase()
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Single();

            return result;
        }

        public async void InsertBusiness(Business business)
        {
            await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business);
        }

        public async void UpdateBusiness(Business business)
        {
            await _connection.AccessDatabase()
                .From<Business>()
                .Update(business);
        }

        public async void DeleteBusiness(int businessId)
        {
            await _connection.AccessDatabase()
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Delete();
        }
    }
}