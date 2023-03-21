using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface ISearchService
    {
        public Task<List<Business>> Search(List<Business> businessList, string searchFrase, ISet<Tag>? tags = null);

        public Task<List<DiscountInfo>> Search(List<DiscountInfo> discountInfos, string searchFrase, ISet<Tag>? tags = null);
    }
}
