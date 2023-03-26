using FoodSaverWebApp.Entities;
using Postgrest;
using Postgrest.Responses;

namespace FoodSaverWebApp.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IDbConnection _connection;
        private IAuthService _authService;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(IDbConnection connection, IAuthService authService, ILogger<BusinessService> logger)
        {
            _connection = connection;
            _authService = authService;
            _logger = logger;
        }
        
        public async Task<ICollection<Business?>> GetAllBusinesses()
        {
            var result = await _connection.AccessDatabase()
                .From<Business>()
                .Get();

            return result.Models;
        }

        public async Task<ICollection<Business?>> GetAllBusinessesUnderAdmin(User user)
        {
            List<object> userBusinessIds = new List<object>();

            foreach (UserBusiness userBusiness in user.Businesses)
            {
                if (userBusiness.IsAdmin)
                    userBusinessIds.Add(userBusiness.BusinessId);
            }
            
            ModeledResponse<Business>? businessResult = await _connection.AccessDatabase()
                .From<Business>()
                .Filter(x => x.BusinessId, Constants.Operator.In, userBusinessIds)
                .Get();

            return businessResult.Models;
        }

        public async Task<Business?> GetBusiness(int businessId)
        {
            Business? result = await _connection.AccessDatabase()
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Single();

            return result;
        }

        public async Task InsertBusiness(Business business)
        {
            await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business);

            List<BusinessTag> businessTags = new List<BusinessTag>();
            foreach (Tag tag in business.Tags)
            {
                businessTags.Add(new BusinessTag
                {
                    BusinessId = business.BusinessId,
                    TagId = tag.TagId
                });
            }

            var bt =  await _connection.AccessDatabase()
                .From<BusinessTag>()
                .Insert(businessTags);
        }
        
        public async Task<Business?> ReturnBusinessOnInsert(Business business)
        {
            ModeledResponse<Business> result =  await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business, new QueryOptions{ Returning = QueryOptions.ReturnType.Representation});

            List<BusinessTag> businessTags = new List<BusinessTag>();
            foreach (Tag tag in business.Tags)
            {
                businessTags.Add(new BusinessTag
                {
                    BusinessId = business.BusinessId,
                    TagId = tag.TagId
                });
            }

            var bt =  await _connection.AccessDatabase()
                .From<BusinessTag>()
                .Insert(businessTags);
            
            return result.Models[0];
        }

        public async Task UpdateBusiness(Business business)
        {
            await _connection.AccessDatabase()
                .From<Business>()
                .Update(business);
        }

        public async Task DeleteBusiness(int businessId)
        {
            // Not a fan for the 4 queries, but delete cascade is not easy to enable
            
            await _connection.AccessDatabase()
                .From<UserBusiness>()
                .Where(x => x.BusinessId == businessId)
                .Delete();

            Business? business = await GetBusiness(businessId);
            await _connection.AccessDatabase()
                .From<Business>()
                .Where(x => x.BusinessId == businessId)
                .Delete();

            if (business != null)
                await _connection.AccessDatabase()
                    .From<Address>()
                    .Where(x => x.AddressId == business.AddressId)
                    .Delete();
        }

        public async Task LinkUserToBusiness(Business business)
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