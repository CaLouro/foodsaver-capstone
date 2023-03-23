using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public delegate string[][] ObjectListTokenizerCallback<T>(List<T> objects);

    public delegate string[] ObjectTokenizerCallback<T>(T obj);

    public class SearchService : ISearchService
    {
        public Task<List<Business>> Search(List<Business> businessList, string searchPhrase,
            ISet<Tag>? tags = null)
        {
            return SetupSearch(businessList, list => TokenizeManyObjects(list, TokenizeBusiness),
                searchPhrase, tags);
        }

        public Task<List<DiscountInfo>> Search(List<DiscountInfo> discountList, string searchPhrase,
            ISet<Tag>? tags = null)
        {
            return SetupSearch(discountList, list => TokenizeManyObjects(list, TokenizeDiscountInfo), searchPhrase,
                tags);
        }

        public Task<List<Item>> Search(List<Item> itemsList, string searchPhrase, ISet<Tag>? tags = null)
        {
            return SetupSearch(itemsList, list => TokenizeManyObjects(list, TokenizeItem), searchPhrase, tags);
        }

        protected async Task<List<T>> SetupSearch<T>(List<T> objects, ObjectListTokenizerCallback<T> objectsTokenizer,
            string searchPhrase,
            ISet<Tag>? tags)
        {
            var searchResults = new List<T>();
            await Task.Run(() =>
            {
                var searchTokens = TokenizeSearchParameters(searchPhrase, tags);
                var objectTokens = objectsTokenizer(objects);

                var results = ActSearch(objects, objectTokens, searchTokens);
                searchResults = results.Values.Reverse().ToList();
            });

            return searchResults;
        }

        /// <summary>
        /// Finds the objects that match specific 'searchTokens' along with how many matches there was.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="objectsTokens"></param>
        /// <param name="searchTokens"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected SortedDictionary<int, T> ActSearch<T>(List<T> objects, string[][] objectsTokens,
            string[] searchTokens)
        {
            var results = new SortedDictionary<int, T>();

            for (int i = 0; i < objects.Count; i++)
            {
                var count = CountTokenMatches(objectsTokens[i], searchTokens);
                if (count > 0)
                {
                    results.Add(count, objects[i]);
                }
            }

            return results;
        }

        /// <summary>
        /// Counts how many of the same tokens exists in both 'objectTokens' and 'searchTokens'.
        /// </summary>
        /// <param name="objectTokens"></param>
        /// <param name="searchTokens"></param>
        /// <returns></returns>
        protected int CountTokenMatches(string[] objectTokens, string[] searchTokens)
        {
            return (
                from oToken in objectTokens
                from sToken in searchTokens
                where oToken == sToken
                select oToken).Count();
        }

        /// <summary>
        /// Transforms the search phrase and the tags into a list of 'tokens', lowercase single-word strings.
        /// </summary>
        /// <param name="searchPhrase"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        protected string[] TokenizeSearchParameters(string? searchPhrase, ISet<Tag>? tags)
        {
            var tokens = new List<string>();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                tokens.AddRange(searchPhrase.Trim().ToLower()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            }

            if (tags != null)
            {
                tokens.AddRange(tags.ToList().ConvertAll(t => t.Name.ToLower()));
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Tokenizes a list of objects with the given callback ('tokenizer').
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="tokenizer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected string[][] TokenizeManyObjects<T>(List<T> objects, ObjectTokenizerCallback<T> tokenizer)
        {
            var tokens = new string[objects.Count][];
            for (int i = 0; i < objects.Count; i++)
            {
                tokens[i] = tokenizer(objects[i]);
            }

            return tokens;
        }

        /// <summary>
        /// Generates an array of tokens based on meaningful information about the Business object. 
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        protected string[] TokenizeBusiness(Business business)
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

        /// <summary>
        /// Generates an array of tokens based on meaningful information about the DiscountInfo object. 
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        protected string[] TokenizeDiscountInfo(DiscountInfo discount)
        {
            return TokenizeItem(discount.Item);
        }

        /// <summary>
        /// Generates an array of tokens based on meaningful information about the Item object. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string[] TokenizeItem(Item item)
        {
            var tokens = new List<string>();

            tokens.AddRange(
                item.Name
                    .ToLower()
                    .Split(" "));

            if (item.Description != null)
            {
                tokens.AddRange(
                    item.Description
                        .ToLower()
                        .Split(" "));
            }

            tokens.AddRange(
                TokenizeBusiness(item.Business));

            return tokens.ToArray();
        }
    }
}