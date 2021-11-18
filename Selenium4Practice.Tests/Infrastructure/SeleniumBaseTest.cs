using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium4Practice.Framework.Configuration;
using Selenium4Practice.Framework.DependencyResolution;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.Grid;
using Selenium4Practice.Framework.NetworkMonitoring;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Selenium4Practice.Pages.Infrastructure;
using System.Collections.Generic;

namespace Selenium4Practice.Tests.Infrastructure;

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

    [SetUp]
    public void SetUp() => ServiceProvider.GetService<ISeleniumNetworkMonitor>().StartMonitoring(ServiceProvider.GetService<IWebDriver>());

    [TearDown]
    public void TearDown()
    {
        var networkMonitor = ServiceProvider.GetService<ISeleniumNetworkMonitor>();
        networkMonitor.StopMonitoring(ServiceProvider.GetService<IWebDriver>());
        GetTestAttachmentHandlers().ForEach(x => x.Execute(TestContext.CurrentContext));
        networkMonitor.ClearNetworkData();
    }

    protected abstract void InitializePageObjects();

    private IList<ITestAttachmentHandler> GetTestAttachmentHandlers() => new List<ITestAttachmentHandler>()
    {
        ServiceProvider.GetService<IJavaScriptLogsTestAttachmentHandler>(),
        ServiceProvider.GetService<INetworkRequestLogsTestAttachmentHandler>(),
        ServiceProvider.GetService<ISeleniumScreenshotTestAttachmentHandler>()
    };
}