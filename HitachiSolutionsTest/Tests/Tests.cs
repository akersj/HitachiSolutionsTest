using FluentAssertions;
using HitachiSolutionsTest.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HitachiSolutionsTest.Tests
{
    public class Tests
    {
        IWebDriver driver;
        private const string homepageUrl = "https://global.hitachi-solutions.com/";
        private const int numberOfPaginatedResultsToTest = 2;
        private const string seleniumDriverPath = @"C:\Users\Jamie\.dotnet\tools";

        [OneTimeSetUp]
        public void Setup()
        {            
            driver = new ChromeDriver(seleniumDriverPath);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CanNavigateToTheSite()
        {
            driver.Navigate().GoToUrl(homepageUrl);
            var homepage = new Homepage(driver);

            homepage.HasTopLeftLogo().Should().BeTrue();
            homepage.HasSearchButton().Should().BeTrue();
        }

        [TestCase("hello world", true)]
        [TestCase("&quot;", true)]
        [TestCase("<b>test</b>", false)]
        [TestCase("", true)]
        [TestCase("1bda519e-8b00-4f11-b07a-10a1e832506e", false)]
        [TestCase("会カメ", false)]        
        [TestCase("مرحبًا", false)]
        public void KeywordsReturnResults(string searchPhrase, bool expectedToHaveResults)
        {
            driver.Navigate().GoToUrl(homepageUrl);
            var homepage = new Homepage(driver);
            homepage.ClickSearchButton();
            homepage.PopulateSearchField(searchPhrase);
            homepage.ClickSubmitSearch();
            var searchResultsPage = new SearchResults(driver);
            
            if (expectedToHaveResults)
            {                
                searchResultsPage.GetTotalCount().Should().BeGreaterThan(0);
                var searchResults = searchResultsPage.GetSearchResults();
                searchResults.Count.Should().BeGreaterThan(0);
            }
            else
            {
                searchResultsPage.GetSearchResults().Count.Should().Be(0);
            }            
        }

        [TestCase("test")]
        public void SearchResultsCanBeOpened(string searchPhrase)
        {
            driver.Navigate().GoToUrl(homepageUrl);
            var homepage = new Homepage(driver);
            homepage.ClickSearchButton();
            homepage.PopulateSearchField(searchPhrase);
            homepage.ClickSubmitSearch();

            var searchResultsPage = new SearchResults(driver);

            bool testNextPage;
            do
            {
                var countOfResults = searchResultsPage.GetPageCount();
                testNextPage = false;
                for (var i = 0; i < countOfResults; i++)
                {
                    var result = searchResultsPage.GetSearchResults()[i];
                    var resultLink = result.GetLink();
                    result.Click();
                    driver.Url.Should().Be(resultLink);
                    driver.Navigate().Back();
                }

                if (searchResultsPage.HasMultiplePages() && searchResultsPage.GetCurrentPage() < numberOfPaginatedResultsToTest)
                {
                    searchResultsPage.ClickNextPageButton();
                    testNextPage = true;
                }
            } while (testNextPage);
        }
    }
}