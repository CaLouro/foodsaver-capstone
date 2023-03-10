using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class TagService : ITagService
    {
        private readonly IDbConnection _connection;
        public TagService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<ICollection<Tag>> GetAllTags()
        {
            var result = await _connection.AccessDatabase()
                .From<Tag>()
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
    }
}