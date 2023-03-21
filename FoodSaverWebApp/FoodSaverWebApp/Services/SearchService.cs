using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class SearchService : ISearchService
    {
        public async Task<List<Business>> Search(List<Business> businessList, string searchFrase, ISet<Tag>? tags = null)
        {
            var searchResult = new List<Business>();

            await Task.Run(() =>
            {
                var searchTokens = BreakdownSearchParameters(searchFrase, tags);
                var objectTokens = BreakdownBusinessList(businessList);

                var results = PerformSearch(businessList, objectTokens, searchTokens);

                searchResult = results.Values.ToList();
            });

            return searchResult;
        }

        public async Task<List<DiscountInfo>> Search(List<DiscountInfo> discountInfos, string searchFrase, ISet<Tag>? tags = null)
        {
            return discountInfos;
        }

        protected string[] BreakdownSearchParameters(string searchFrase, ISet<Tag>? tags)
        {
            var tokens = new List<string>(searchFrase.Split(" "));

            if (tags != null)
            {
                tokens.AddRange(tags.ToList().ConvertAll(t => t.Name));
            }

            return tokens.ToArray();
        }

        protected string[][] BreakdownBusinessList(List<Business> businessList)
        {
            var tokens = new string[businessList.Count][];

            for (int i = 0; i < businessList.Count; i++)
            {
                tokens[i] = BreakdownBusiness(businessList[i]);
            }

            return tokens;
        }

        protected string[] BreakdownBusiness(Business business)
        {
            var tokens = new List<string>();

            tokens.AddRange(
                business.Name
                    .ToLower()
                    .Split(" "));

            tokens.AddRange(
                business.Address
                    .PresentAddress(showProvince: true, showPostalCode: true)
                    .Replace(",", "")
                    .Split(" "));

            tokens.AddRange(
                business.Tags
                    .ConvertAll(t => t.Name));

            if (business.ContactEmail != null)
            {
                tokens.Add(business.ContactEmail);
            }

            if (business.ContactPhone != null)
            {
                tokens.Add(business.ContactPhone);
            }

            return tokens.ToArray();
        }

        protected SortedList<int, T> PerformSearch<T>(List<T> list, string[][] objectsTokens, string[] searchTokens)
        {
            var results = new SortedList<int, T>();

            for (int i = 0; i <= list.Count; i++)
            {
                var count = CountTokenMatches(objectsTokens[i], searchTokens);
                if (count > 0)
                {
                    results.Add(count, list[i]);
                }
            }

            return results;
        }

        protected int CountTokenMatches(string[] objectTokens, string[] searchTokens)
        {
            int count = 0;

            foreach (var oToken in objectTokens)
            {
                foreach (var sToken in searchTokens)
                {
                    if (oToken == sToken)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
