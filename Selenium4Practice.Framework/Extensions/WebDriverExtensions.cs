using OpenQA.Selenium;

namespace Selenium4Practice.Framework.Extensions;

public static class WebDriverExtensions
{
    public static bool TryGetelement(this IWebDriver webDriver, By locator, out IWebElement element)
    {
        try
        {
            element = webDriver.FindElement(locator);
            return true;
        }
        catch (WebDriverException)
        {
            element = null;
            return false;
        }
    }

    public static bool IsElementDisplayed(this IWebDriver webDriver, By locator) => webDriver.TryGetelement(locator, out var element) ? element.Displayed : false;
}