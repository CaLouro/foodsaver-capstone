using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class SearchService : ISearchService
    {
        public async Task<List<Business>> Search(List<Business> businessList, string searchPhrase, ISet<Tag>? tags = null)
        {
            var searchResult = new List<Business>();
            await Task.Run(() =>
            {
                var searchTokens = BreakdownSearchParameters(searchPhrase, tags);
                var objectTokens = BreakdownBusinessList(businessList);

                var results = PerformSearch(businessList, objectTokens, searchTokens);

                searchResult = results.Values.Reverse().ToList();
            });
            return searchResult;
        }

        public async Task<List<DiscountInfo>> Search(List<DiscountInfo> discountInfos, string searchPhrase, ISet<Tag>? tags = null)
        {
            var searchResult = new List<DiscountInfo>();
            await Task.Run(() =>
            {
                var searchTokens = BreakdownSearchParameters(searchPhrase, tags);
                var objectTokens = BreakdownDiscountInfoList(discountInfos);

                var results = PerformSearch(discountInfos, objectTokens, searchTokens);

                searchResult = results.Values.Reverse().ToList();
            });
            return searchResult;
        }

        protected string[] BreakdownSearchParameters(string searchPhrase, ISet<Tag>? tags)
        {
            var tokens = new List<string>(searchPhrase.ToLower().Split(" "));
            if (tags != null)
            {
                tokens.AddRange(tags.ToList().ConvertAll(t => t.Name.ToLower()));
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
                    .ToLower()
                    .Replace(",", "")
                    .Split(" "));

            tokens.AddRange(
                business.Tags
                    .ConvertAll(t => t.Name.ToLower()));

            if (business.ContactEmail != null)
            {
                tokens.Add(business.ContactEmail.ToLower());
            }

            if (business.ContactPhone != null)
            {
                tokens.Add(business.ContactPhone.ToLower());
            }

            return tokens.ToArray();
        }

        protected string[][] BreakdownDiscountInfoList(List<DiscountInfo> discountList)
        {
            var tokens = new string[discountList.Count][];
            for (int i = 0; i < discountList.Count; i++)
            {
                tokens[i] = BreakdownDiscountInfo(discountList[i]);
            }
            return tokens;
        }

        protected string[] BreakdownDiscountInfo(DiscountInfo discount)
        {
            var tokens = new List<string>();

            tokens.AddRange(
                discount.Item.Name
                    .ToLower()
                    .Split(" "));

            if (discount.Item.Description != null)
            {
                tokens.AddRange(
                    discount.Item.Description
                        .ToLower()
                        .Split(" "));
            }

            tokens.AddRange(
                BreakdownBusiness(discount.Item.Business));

            return tokens.ToArray();
        }

        protected SortedDictionary<int, T> PerformSearch<T>(List<T> objects, string[][] objectsTokens, string[] searchTokens)
        {
            var results = new SortedDictionary<int, T>();
            for (int i = 0; i <= objects.Count; i++)
            {
                var count = CountTokenMatches(objectsTokens[i], searchTokens);
                if (count > 0)
                {
                    results.Add(count, objects[i]);
                }
            }
            return results;
        }

        protected int CountTokenMatches(string[] objectTokens, string[] searchTokens)
        {
            return (
                from oToken in objectTokens 
                from sToken in searchTokens 
                where oToken == sToken 
                select oToken).Count();
        }
    }
}
