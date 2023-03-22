namespace FoodSaverWebAppTest.ServicesTests
{
    [TestFixture]
    public class SearchServiceTest : SearchService
    {
        [TestCase("coffee", ExpectedResult = new string[] { "coffee" })]
        [TestCase("Coffee", ExpectedResult = new string[] { "coffee" })]
        [TestCase("COFFEE", ExpectedResult = new string[] { "coffee" })]
        [TestCase("Coffee Shop", ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(" Coffee Shop", ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase("Coffee Shop ", ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase("     Coffee Shop ", ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase("Coffee  Shop", ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase("", ExpectedResult = new string[] { })]
        [TestCase(" ", ExpectedResult = new string[] { })]
        public string[] TokenizeSearchParameters_SearchStringNoTags(string searchInput)
        {
            return TokenizeSearchParameters(searchInput, null);
        }

        [TestCase("", new string[] { "Coffee" },
            ExpectedResult = new string[] { "coffee" })]
        [TestCase("", new string[] { "Coffee", "Cake" },
            ExpectedResult = new string[] { "coffee", "cake" })]
        [TestCase("Coffee", new string[] { "Cake" },
            ExpectedResult = new string[] { "coffee", "cake" })]
        [TestCase("", new string[] { },
            ExpectedResult = new string[] { })]
        [TestCase("", new string[] { "Cake" },
            ExpectedResult = new string[] { "cake" })]
        public string[] TokenizeSearchParameters_SearchStringWithTags(string searchInput, string[] tags)
        {
            var tagSet = new HashSet<Tag>();
            foreach (var tag in tags)
            {
                tagSet.Add(new Tag { Name = tag });
            }

            return TokenizeSearchParameters(searchInput, tagSet);
        }
    }
}