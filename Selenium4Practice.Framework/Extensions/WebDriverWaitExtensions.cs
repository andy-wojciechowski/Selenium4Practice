using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Selenium4Practice.Framework.Extensions
{
    public static class WebDriverWaitExtensions
    {
        public static void WaitForElementNotToBePresent(this WebDriverWait webDriverWait, By locator)
        {
            webDriverWait.Until(d => 
            {
                try
                {
                    d.FindElement(locator);
                    return false;
                }
                catch (WebDriverException)
                {
                    return true;
                }
            });
        }
    }
}