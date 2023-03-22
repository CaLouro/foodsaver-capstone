using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface ISearchService
    {
        public Task<List<Business>> Search(List<Business> businessList, string searchPhrase, ISet<Tag>? tags = null);

        public Task<List<DiscountInfo>> Search(List<DiscountInfo> discountList, string searchPhrase,
            ISet<Tag>? tags = null);

        public Task<List<Item>> Search(List<Item> itemsList, string searchPhrase, ISet<Tag>? tags = null);
    }
}
