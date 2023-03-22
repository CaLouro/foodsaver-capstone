namespace FoodSaverWebAppTest.ServicesTests
{
    [TestFixture]
    public class SearchServiceTest : SearchService
    {
        [TestCase("Coffee", ExpectedResult = new string[] { "coffee" })]
        [TestCase("Coffee Shop", ExpectedResult = new string[] { "coffee", "shop" })]
        public string[] BreakdownSearchParameters_SearchStringNoTags(string searchInput)
        {
            return TokenizeSearchParameters(searchInput, null);
        }
    }
}