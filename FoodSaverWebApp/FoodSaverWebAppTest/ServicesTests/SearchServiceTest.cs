// ReSharper disable RedundantExplicitArrayCreation

namespace FoodSaverWebAppTest.ServicesTests
{
    [TestFixture]
    public class SearchServiceTest : SearchService
    {
        #region TokenizeSearchParameters Tests

        [TestCase(
            "coffee",
            ExpectedResult = new string[] { "coffee" })]
        [TestCase(
            "Coffee",
            ExpectedResult = new string[] { "coffee" })]
        [TestCase(
            "COFFEE",
            ExpectedResult = new string[] { "coffee" })]
        [TestCase(
            "Coffee Shop",
            ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(
            " Coffee Shop",
            ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(
            "Coffee Shop ",
            ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(
            "     Coffee Shop ",
            ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(
            "Coffee  Shop",
            ExpectedResult = new string[] { "coffee", "shop" })]
        [TestCase(
            "",
            ExpectedResult = new string[] { })]
        [TestCase(
            " ",
            ExpectedResult = new string[] { })]
        public string[] TokenizeSearchParameters_SearchStringNoTags(string searchInput)
        {
            return TokenizeSearchParameters(searchInput, null);
        }

        [TestCase(
            "",
            new string[] { "Coffee" },
            ExpectedResult = new string[] { "coffee" })]
        [TestCase(
            "",
            new string[] { "Coffee", "Cake" },
            ExpectedResult = new string[] { "coffee", "cake" })]
        [TestCase(
            "Coffee",
            new string[] { "Cake" },
            ExpectedResult = new string[] { "coffee", "cake" })]
        [TestCase(
            "",
            new string[] { },
            ExpectedResult = new string[] { })]
        [TestCase(
            "",
            new string[] { "Cake" },
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

        #endregion

        #region CountTokenMatches Tests

        [TestCase(
            new string[] { },
            new string[] { },
            ExpectedResult = 0)]
        [TestCase(
            new string[] { },
            new string[] { "pizza", "italian" },
            ExpectedResult = 0)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { },
            ExpectedResult = 0)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "pizza", "italian" },
            ExpectedResult = 0)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "coffee" },
            ExpectedResult = 1)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "drinks" },
            ExpectedResult = 1)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "coffee", "shop" },
            ExpectedResult = 2)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "shop", "art" },
            ExpectedResult = 2)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "coffee", "shop", "drinks", "art" },
            ExpectedResult = 4)]
        [TestCase(
            new string[] { "coffee", "shop", "drinks", "art" },
            new string[] { "art", "coffee", "drinks", "shop" },
            ExpectedResult = 4)]
        public int CountTokenMatches_VariousInputs(string[] objectTokens, string[] searchTokens)
        {
            return CountTokenMatches(objectTokens, searchTokens);
        }

        #endregion

        #region TokenizeManyObjects Tests

        [TestCaseSource(typeof(TokenizeManyObjectsTestData), nameof(TokenizeManyObjectsTestData.IntegerValues))]
        public string[][] TokenizeManyObjects_IntegerValues(int[] objects)
        {
            string[] Tokenizer(int value) => new[] { value.ToString() };

            return TokenizeManyObjects(objects.ToList(), Tokenizer);
        }

        [TestCaseSource(typeof(TokenizeManyObjectsTestData), nameof(TokenizeManyObjectsTestData.BusinessValues))]
        public string[][] TokenizeManyObjects_BusinessValues(Business[] objects)
        {
            return TokenizeManyObjects(objects.ToList(), TokenizeBusiness);
        }

        /// <summary>
        /// This class generates test case data for <see cref="TokenizeManyObjects_IntegerValues"/>.
        /// </summary>
        private class TokenizeManyObjectsTestData
        {
            public static IEnumerable IntegerValues
            {
                get
                {
                    yield return new TestCaseData(Array.Empty<int>())
                        .Returns(Array.Empty<string[]>());

                    yield return new TestCaseData(new int[] { 1 })
                        .Returns(new string[][] { new string[] { "1" } });
                }
            }

            public static IEnumerable BusinessValues
            {
                get
                {
                    var business1 = new Business
                    {
                        Name = "Cool Store",
                        Address = new Address
                        {
                            Line1 = "123 Apple Street",
                            City = "Toronto",
                            ProvinceCode = "ON",
                            PostalCode = "A1B2C3"
                        }
                    };

                    var business1ExpectedTokens = new string[]
                    {
                        "cool",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    };

                    yield return new TestCaseData(arg: new Business[] { business1 })
                        .Returns(new string[][] { business1ExpectedTokens });

                    var business2 = new Business
                    {
                        Name = "Corner Diner Family Restaurant",
                        Address = new Address
                        {
                            Line1 = "101 Busy Av",
                            City = "Waterloo",
                            ProvinceCode = "ON",
                            PostalCode = "L0K9J8"
                        }
                    };

                    var business2ExpectedTokens = new string[]
                    {
                        "corner",
                        "diner",
                        "family",
                        "restaurant",
                        "101",
                        "busy",
                        "av",
                        "waterloo",
                        "on",
                        "l0k9j8"
                    };

                    yield return new TestCaseData(arg: new Business[] { business1, business2 })
                        .Returns(new string[][] { business1ExpectedTokens, business2ExpectedTokens });
                }
            }
        }

        #endregion

        #region TokenizeBusiness Tests

        [TestCaseSource(typeof(TokenizeBusinessTestData), nameof(TokenizeBusinessTestData.BusinessValues))]
        public string[] TokenizeBusiness_BusinessValues(Business business)
        {
            return TokenizeBusiness(business);
        }

        private class TokenizeBusinessTestData
        {
            public static IEnumerable BusinessValues
            {
                get
                {
                    yield return new TestCaseData(new Business
                    {
                        Name = "Test Store",
                        Address = new Address
                        {
                            Line1 = "123 Apple Street",
                            City = "Toronto",
                            ProvinceCode = "ON",
                            PostalCode = "A1B2C3"
                        }
                    }).Returns(new string[]
                    {
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    });

                    yield return new TestCaseData(new Business
                    {
                        Name = "Test Store",
                        Address = new Address
                        {
                            Line1 = "123 Apple",
                            Line2 = "Street",
                            City = "Toronto",
                            ProvinceCode = "ON",
                            PostalCode = "A1B2C3"
                        }
                    }).Returns(new string[]
                    {
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    });

                    yield return new TestCaseData(new Business
                    {
                        Name = "Test Store",
                        ContactEmail = "business@contact.com",
                        Address = new Address
                        {
                            Line1 = "123 Apple",
                            Line2 = "Street",
                            City = "Toronto",
                            ProvinceCode = "ON",
                            PostalCode = "A1B2C3"
                        }
                    }).Returns(new string[]
                    {
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3",
                        "business@contact.com"
                    });

                    yield return new TestCaseData(new Business
                    {
                        Name = "Test Store",
                        ContactEmail = "business@contact.com",
                        ContactPhone = "(123) 123-1234",
                        Address = new Address
                        {
                            Line1 = "123 Apple",
                            Line2 = "Street",
                            City = "Toronto",
                            ProvinceCode = "ON",
                            PostalCode = "A1B2C3"
                        }
                    }).Returns(new string[]
                    {
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3",
                        "business@contact.com",
                        "(123) 123-1234"
                    });
                }
            }
        }

        #endregion

        #region TokenizeItem Tests

        [TestCaseSource(typeof(TokenizeItemTestData), nameof(TokenizeItemTestData.BusinessValues))]
        public string[] TokenizeItem_BusinessValues(Item item)
        {
            return TokenizeItem(item);
        }

        private class TokenizeItemTestData
        {
            public static IEnumerable BusinessValues
            {
                get
                {
                    yield return new TestCaseData(new Item
                    {
                        Name = "Espresso",
                        Business = new Business
                        {
                            Name = "Test Store",
                            Address = new Address
                            {
                                Line1 = "123 Apple Street",
                                City = "Toronto",
                                ProvinceCode = "ON",
                                PostalCode = "A1B2C3"
                            }
                        }
                    }).Returns(new string[]
                    {
                        "espresso",
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    });

                    yield return new TestCaseData(new Item
                    {
                        Name = "Single Espresso",
                        Business = new Business
                        {
                            Name = "Test Store",
                            Address = new Address
                            {
                                Line1 = "123 Apple Street",
                                City = "Toronto",
                                ProvinceCode = "ON",
                                PostalCode = "A1B2C3"
                            }
                        }
                    }).Returns(new string[]
                    {
                        "single",
                        "espresso",
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    });

                    yield return new TestCaseData(new Item
                    {
                        Name = "Single Espresso",
                        Description = "A cup of nice and hot single espresso.",
                        Business = new Business
                        {
                            Name = "Test Store",
                            Address = new Address
                            {
                                Line1 = "123 Apple Street",
                                City = "Toronto",
                                ProvinceCode = "ON",
                                PostalCode = "A1B2C3"
                            }
                        }
                    }).Returns(new string[]
                    {
                        "single",
                        "espresso",
                        "a",
                        "cup",
                        "of",
                        "nice",
                        "and",
                        "hot",
                        "single",
                        "espresso.",
                        "test",
                        "store",
                        "123",
                        "apple",
                        "street",
                        "toronto",
                        "on",
                        "a1b2c3"
                    });
                }
            }
        }

        #endregion
    }
}