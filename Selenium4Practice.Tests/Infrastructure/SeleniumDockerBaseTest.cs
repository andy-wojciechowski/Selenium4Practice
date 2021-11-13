using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium4Practice.Framework.Configuration;
using Selenium4Practice.Framework.DependencyResolution;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.Grid;
using Selenium4Practice.Pages.Infrastructure;

namespace Selenium4Practice.Tests.Infrastructure;

[TestFixture(Browser.Chrome)]
[TestFixture(Browser.Firefox)]
[TestFixture(Browser.Edge)]
public abstract class SeleniumDockerBaseTest
{
    private Browser Browser { get; }
    protected IWebDriver WebDriver { get; set; }
    protected ServiceProvider ServiceProvider { get; set; }

    public SeleniumDockerBaseTest(Browser browser)
    {
        Browser = browser;
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SeleniumDockerGridManager.EnsureGridIsStarted(ConfigSettings.DockerComposePath, ConfigSettings.SeleniumGridContainerName);
        var seleniumObjectConfig = new SeleniumObjectConfiguration() { PageBaseUrl = ConfigSettings.BaseUrl };
        ServiceProvider = new ServiceCollection()
            .AddWebDriver(Browser, ConfigSettings.SeleniumServerUrl)
            .AddSeleniumObjectsContainingTypes(seleniumObjectConfig, typeof(IPageObjectAssemblyMarker))
            .AddNetworkMonitoring()
            .AddTestAttachmentHandlers()
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