using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;

namespace HitachiSolutionsTest
{
    public class ChildFinder : IElementLocator
    {
        private readonly ISearchContext searchContext;
        private readonly IWebElement Root;

        public ChildFinder(IWebElement root, IWebDriver driver)
        {
            this.Root = root;
            this.searchContext = driver;
        }

        public ISearchContext SearchContext
        {
            get { return this.searchContext; }
        }
        public IWebElement LocateElement(IEnumerable<By> bys)
        {
            Exception? exception = null;
            foreach (var by in bys)
            {
                try
                {
                    return this.Root.FindElement(by);
                }
                catch (NoSuchElementException e)
                {
                    exception = e;
                }
            }
            if (exception is not null)
            {
                throw exception;
            }
            throw new ApplicationException("Element was not found");
        }

        public ReadOnlyCollection<IWebElement> LocateElements(IEnumerable<By> bys)
        {
            Exception? exception = null;
            var FoundElements = new List<IWebElement>();
            foreach (var by in bys)
            {
                try
                {
                    FoundElements.AddRange(this.Root.FindElements(by));
                }
                catch (NoSuchElementException e)
                {
                    exception = e;
                }
            }
            if (FoundElements.Count > 0)
            {
                return FoundElements.AsReadOnly();
            }
            if (exception != null)
            {
                throw exception;
            }
            return new ReadOnlyCollection<IWebElement>(new List<IWebElement>() { });
        }
    }
}
