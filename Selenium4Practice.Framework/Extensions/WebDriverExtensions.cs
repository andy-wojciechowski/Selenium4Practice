using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium4Practice.Framework.Extensions;

public static class WebDriverExtensions
{
    private const string DevToolsCapability = "se:cdp";

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

    public static bool IsElementDisplayed(this IWebDriver webDriver, By locator) => webDriver.TryGetelement(locator, out var element) && element.Displayed;

    public static bool HasDevToolsCapability(this IWebDriver webDriver)
    {
        var remoteWebDriver = (RemoteWebDriver)webDriver;
        return remoteWebDriver.Capabilities.HasCapability(DevToolsCapability);
    }
}