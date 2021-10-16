using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium4Practice.Pages
{
    public abstract class BasePage
    {
        private IWebDriver WebDriver { get; set; }
        private WebDriverWait WebDriverWait { get; set; }

        public BasePage(IWebDriver webDriver!)
        {
            WebDriver = webDriver;
            WebDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60));
        }

        public abstract bool IsAt();

        public abstract void Navigate();

        public abstract void WaitForPage();
    }
}