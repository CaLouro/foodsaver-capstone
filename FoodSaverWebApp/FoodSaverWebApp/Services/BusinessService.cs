using FoodSaverWebApp.Entities;
using Postgrest;
using Postgrest.Responses;

namespace FoodSaverWebApp.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IDbConnection _connection;
        private IAuthService _authService;
        public BusinessService(IDbConnection connection, IAuthService authService)
        {
            _connection = connection;
            _authService = authService;
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
        
        public async Task<Business?> ReturnBusinessOnInsert(Business business)
        {
            ModeledResponse<Business> result =  await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business, new QueryOptions{ Returning = QueryOptions.ReturnType.Representation});

            return result.Models[0];
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

        public async void LinkUserToBusiness(Business business)
        {
            User? user = await _authService.GetActiveUser();

            UserBusiness userBusiness = new UserBusiness()
            {
                UserId = user.UserId,
                BusinessId = business.BusinessId,
                IsAdmin = true
            };
            
            await _connection.AccessDatabase()
                .From<UserBusiness>()
                .Insert(userBusiness);
        }
    }
}