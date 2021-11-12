using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Selenium4Practice.Framework.Enums;
using System;

namespace Selenium4Practice.Framework.WebDriver
{
    public static class DriverFactory
    {
        private static readonly string FirefoxBinaryLocation = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";

        public static IWebDriver CreateWebDriver(Browser browser, string seleniumServerUrl) =>
            browser switch 
            {
                Browser.Chrome => new RemoteWebDriver(new Uri(seleniumServerUrl), new ChromeOptions()),
                Browser.Firefox => new RemoteWebDriver(new Uri(seleniumServerUrl), new FirefoxOptions() { BrowserExecutableLocation = FirefoxBinaryLocation }),
                Browser.Edge => new RemoteWebDriver(new Uri(seleniumServerUrl), new EdgeOptions()),
                _ => throw new ArgumentNullException(nameof(browser))
            };
    }
}