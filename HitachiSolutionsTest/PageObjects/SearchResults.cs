using HitachiSolutionsTest.PageObjects.SearchResultsObjects;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace HitachiSolutionsTest.PageObjects
{
    public class SearchResults
    {
        [FindsBy(How = How.ClassName, Using = "search-result")]
        private readonly IList<IWebElement> SearchResultElements;

        [FindsBy(How = How.XPath, Using = "//div[@class='results-header']/p/strong")]
        private readonly IList<IWebElement> Counters;

        [FindsBy(How = How.ClassName, Using = "page-numbers")]
        private readonly IList<IWebElement> PageNumbers;

        [FindsBy(How = How.CssSelector, Using = ".next.page-numbers")]
        private readonly IWebElement NextPageButton;

        [FindsBy(How = How.CssSelector, Using = ".page-numbers.current")]
        private readonly IWebElement CurrentPage;

        private readonly IWebDriver Driver;
        public SearchResults(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public List<SearchResult> GetSearchResults()
        {
            return this.SearchResultElements.Select(x => new SearchResult(this.Driver, x)).ToList();
        }        

        public int GetTotalCount()
        {
            return int.Parse(this.Counters[2].Text);
        }

        public int GetPageCount()
        {
            return int.Parse(this.Counters[1].Text) - int.Parse(this.Counters[0].Text) + 1;
        }

        public bool HasMultiplePages()
        {
            return this.PageNumbers.Count > 0;
        }

        public void ClickNextPageButton()
        {
            this.NextPageButton.Click();
        }

        public int GetCurrentPage()
        {
            return int.Parse(this.CurrentPage.Text);
        }
    }
}
