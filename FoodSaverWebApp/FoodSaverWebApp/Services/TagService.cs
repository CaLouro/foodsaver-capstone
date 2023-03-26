using FoodSaverWebApp.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Postgrest;

namespace FoodSaverWebApp.Services
{
    public class TagService : ITagService
    {
        private readonly IDbConnection _connection;
        private readonly IBusinessService _businessService;
        public TagService(IDbConnection connection, IBusinessService businessService)
        {
            _connection = connection;
            _businessService = businessService;
        }
        
        public async Task<ICollection<Tag>> GetAllTags()
        {
            var result = await _connection.AccessDatabase()
                .From<Tag>()
                .Order(t => t.Name, Constants.Ordering.Ascending)
                .Get();

            return result.Models;
        }

        public async Task<Tag?> GetTag(int tagId)
        {
            Tag? result = await _connection.AccessDatabase()
                .From<Tag>()
                .Where(x => x.TagId == tagId)
                .Single();

            return result;
        }

        public async Task<ICollection<Tag>> GetTagsUnderBusiness(int businessId)
        {
            Business? business = await _businessService.GetBusiness(businessId);

            if (business == null)
                return new List<Tag>();

            return business.Tags;
        }
    }
}