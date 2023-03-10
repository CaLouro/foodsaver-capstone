using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface ITagService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for Tag entities
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Tag>> GetAllTags();
        
        /// <summary>
        /// Retrieves a single Tag entity given the tagId
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public Task<Tag?> GetTag(int tagId);
    }
}