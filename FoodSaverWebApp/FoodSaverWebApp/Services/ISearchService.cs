using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface ISearchService
    {
        /// <summary>
        /// Filters a list of <see cref="Business"/> based on a search phrase. A set of tags can be given to enhance
        /// the search.
        ///
        /// The <see cref="searchPhrase"/> can be null, empty or whitespaces but will result in empty results if no tags
        /// are provided.
        /// </summary>
        /// <param name="businessList"></param>
        /// <param name="searchPhrase"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Task<List<Business>> Search(List<Business> businessList, string searchPhrase, ISet<Tag>? tags = null);

        /// <summary>
        /// Filters a list of <see cref="DiscountInfo"/> based on a search phrase. A set of tags can be given to enhance
        /// the search.
        ///
        /// The <see cref="searchPhrase"/> can be null, empty or whitespaces but will result in empty results if no tags
        /// are provided.
        /// </summary>
        /// <param name="discountList"></param>
        /// <param name="searchPhrase"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Task<List<DiscountInfo>> Search(List<DiscountInfo> discountList, string searchPhrase,
            ISet<Tag>? tags = null);

        /// <summary>
        /// Filters a list of <see cref="Item"/> based on a search phrase. A set of tags can be given to enhance
        /// the search.
        ///
        /// The <see cref="searchPhrase"/> can be null, empty or whitespaces but will result in empty results if no tags
        /// are provided.
        /// </summary>
        /// <param name="itemsList"></param>
        /// <param name="searchPhrase"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Task<List<Item>> Search(List<Item> itemsList, string searchPhrase, ISet<Tag>? tags = null);
    }
}
