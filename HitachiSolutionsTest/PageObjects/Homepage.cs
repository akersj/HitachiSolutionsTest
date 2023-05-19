using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace HitachiSolutionsTest.PageObjects
{
    public class Homepage
    {
        [FindsBy(How = How.Id, Using = "open-global-search")]
        private readonly IList<IWebElement> SearchButton;

        [FindsBy(How = How.Id, Using = "site-search-keyword")]
        private readonly IWebElement SearchField;

        [FindsBy(How = How.ClassName, Using = "search-form-submit")]
        private readonly IWebElement SearchFormSubmitButton;

        [FindsBy(How = How.ClassName, Using = "hitachi-solutions-logo")]
        private readonly IList<IWebElement> TopLeftLogo;

        public Homepage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public void ClickSearchButton()
        {
            this.SearchButton[0].Click();
        }

        public void PopulateSearchField(string searchPhrase)
        {
            this.SearchField.SendKeys(searchPhrase);
        }

        public void ClickSubmitSearch()
        {
            this.SearchFormSubmitButton.Click();
        }

        public bool HasTopLeftLogo()
        {
            return this.TopLeftLogo.Count > 0;
        }

        public bool HasSearchButton()
        {
            return this.SearchButton.Count > 0;
        }

    }
}
