using NUnit.Framework;
using Selenium4Practice.Framework.Configuration;
using Selenium4Practice.Framework.DependencyResolution;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.Grid;
using Selenium4Practice.Framework.WebDriver;
using Selenium4Practice.Pages.Infrastructure;
using OpenQA.Selenium;
using Microsoft.Extensions.DependencyInjection;

namespace Selenium4Practice.Tests.Infrastructure
{
    [TestFixture(Browser.Chrome)]
    [TestFixture(Browser.Firefox)]
    [TestFixture(Browser.Edge)]
    public abstract class SeleniumBaseTest
    {
        private Browser Browser { get; }
        protected IWebDriver WebDriver { get; set; }
        protected ServiceProvider ServiceProvider { get; set; }

        public SeleniumBaseTest(Browser browser)
        {
            Browser = browser;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SeleniumGridManager.EnsureGridIsStarted(ConfigSettings.SeleniumGridJarPath);
            var seleniumObjectConfig = new SeleniumObjectConfiguration() { PageBaseUrl = ConfigSettings.BaseUrl };
            ServiceProvider = new ServiceCollection()
                .AddSingleton(typeof(IWebDriver), _ => DriverFactory.CreateWebDriver(Browser, ConfigSettings.SeleniumServerUrl, true))
                .AddSeleniumObjectsContainingTypes(seleniumObjectConfig, typeof(IPageObjectAssemblyMarker))
                .BuildServiceProvider();
            InitializePageObjects();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (ServiceProvider != null)
            {
                var webDriver = ServiceProvider.GetService<IWebDriver>();
                webDriver.Quit();
                ServiceProvider.Dispose();
            }
        }

        protected abstract void InitializePageObjects();
    }
}