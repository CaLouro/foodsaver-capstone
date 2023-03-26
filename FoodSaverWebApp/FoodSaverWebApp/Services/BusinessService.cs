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
            ModeledResponse<Business> result = await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business);

            List<BusinessTag> businessTags = new List<BusinessTag>();
            foreach (Tag tag in business.Tags)
            {
                businessTags.Add(new BusinessTag
                {
                    BusinessId = result.Models[0].BusinessId,
                    TagId = tag.TagId
                });
            }

            var bt =  await _connection.AccessDatabase()
                .From<BusinessTag>()
                .Insert(businessTags);
        }
        
        public async Task<Business?> ReturnBusinessOnInsert(Business business)
        {
            ModeledResponse<Business> result = await _connection.AccessDatabase()
                .From<Business>()
                .Insert(business, new QueryOptions{ Returning = QueryOptions.ReturnType.Representation});

            List<BusinessTag> businessTags = new List<BusinessTag>();
            foreach (Tag tag in business.Tags)
            {
                businessTags.Add(new BusinessTag
                {
                    BusinessId = result.Models[0].BusinessId,
                    TagId = tag.TagId
                });
            }
            
            await _connection.AccessDatabase()
                .From<BusinessTag>()
                .Insert(businessTags);
            
            return result.Models[0];
        }

        public async Task UpdateBusiness(Business business)
        {
            _logger.LogInformation("--------------- Updating Business ---------------");
            await _connection.AccessDatabase()
                .From<Business>()
                .Update(business);

            // Get all the connections with tags for a specific business
            ModeledResponse<BusinessTag> businessTags = await _connection.AccessDatabase()
                .From<BusinessTag>()
                .Where(bt => bt.BusinessId == business.BusinessId)
                .Get();

            // Delete business_tag connection if deselected
            foreach (BusinessTag btag in businessTags.Models)
            {
                if (business.Tags.All(b => b.TagId != btag.TagId) && btag.BusinessId == business.BusinessId)
                {
                    _logger.LogInformation($"Tag {btag.TagId} is no longer in the business [REMOVE]");
                    
                    // Deleting business_tag connection
                    // [DON'T LIKE THIS, couldn't find anything else regarding bulk deleting]
                    await _connection.AccessDatabase()
                        .From<BusinessTag>()
                        .Where(b => b.BusinessId == business.BusinessId && b.TagId == btag.TagId)
                        .Delete();
                }
            }

            // Add business_tag connection if selected
            List<BusinessTag> businessTagsToAdd = new List<BusinessTag>();
            foreach (Tag tag in business.Tags)
            {
                if (businessTags.Models.All(b => b.TagId != tag.TagId && b.BusinessId == business.BusinessId))
                {
                    _logger.LogInformation($"Tag {tag.TagId} is not in the BusinessTag table [ADD]");
                    businessTagsToAdd.Add(new BusinessTag
                    {
                        BusinessId = business.BusinessId,
                        TagId = tag.TagId
                    });
                }
            }

            if (businessTagsToAdd.Count != 0)
                await _connection.AccessDatabase()
                    .From<BusinessTag>()
                    .Insert(businessTagsToAdd);
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