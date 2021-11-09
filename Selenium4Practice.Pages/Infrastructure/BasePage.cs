using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Interfaces;

namespace Selenium4Practice.Pages.Infrastructure
{
    public abstract class BasePage : IPage
    {
        #region Properties

        public IWebDriver WebDriver { get; set; }
        public WebDriverWait WebDriverWait { get; set; }
        public string BaseUrl { get; set; }
        public string PageUrl { get; set; }
        public string FullUrl => BaseUrl + PageUrl;

        #endregion

        #region Public Methods

        public virtual void Navigate()
        {
            WebDriver.Navigate().GoToUrl(FullUrl);
            WaitForPage();
        }
        public abstract void WaitForPage();
        public virtual bool IsAt() => WebDriver.Url == FullUrl;

        #endregion
    }
}