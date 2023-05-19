using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace HitachiSolutionsTest.PageObjects.SearchResultsObjects
{
    public class SearchResult
    {
        [FindsBy(How = How.TagName, Using = "a")]
        private readonly IWebElement Link;
        
        public SearchResult(IWebDriver driver, IWebElement root)
        {            
            PageFactory.InitElements(this, new ChildFinder(root, driver));
        }

        public string GetTitle()
        {
            return Link.Text;
        }

        public void Click()
        {            
            this.Link.Click();
        }

        public string GetLink()
        {
            return this.Link.GetAttribute("href");
        }
    }
}
