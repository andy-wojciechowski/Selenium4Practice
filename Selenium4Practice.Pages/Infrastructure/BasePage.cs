using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Interfaces;

namespace Selenium4Practice.Pages
{
    public abstract class BasePage : IPage
    {
        #region Properties

        public IWebDriver WebDriver { get; set; }
        public WebDriverWait WebDriverWait { get; set; }

        #endregion

        #region Public Methods

        public virtual void Navigate()
        {
            throw new System.NotImplementedException();
        }
        public abstract void WaitForPage();
        public virtual bool IsAt() => throw new System.NotImplementedException();

        #endregion
    }
}