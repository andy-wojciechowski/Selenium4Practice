using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium4Practice.Framework.DependencyResolution;
using Selenium4Practice.Framework.DevToolsMonitors.Interfaces;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.Grid;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Selenium4Practice.Pages.Infrastructure;
using System;
using System.Collections.Generic;

namespace Selenium4Practice.Tests.Infrastructure;

[TestFixture(Browser.Chrome)]
[TestFixture(Browser.Firefox)]
[TestFixture(Browser.Edge)]
public abstract class SeleniumBaseTest
{
    private Browser Browser { get; }
    protected IWebDriver WebDriver { get; set; }
    protected IServiceProvider ServiceProvider { get; set; }

    public SeleniumBaseTest(Browser browser)
    {
        Browser = browser;
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var config = SeleniumSetupFixture.Config;
        SeleniumGridManager.EnsureGridIsStarted(config["SeleniumGridJarPath"]);
        ServiceProvider = new ServiceCollection()
            .AddSingleton(config)
            .AddWebDriver(Browser, config["SeleniumServerUrl"], true)
            .AddSeleniumObjectsContainingTypes(typeof(IPageObjectAssemblyMarker))
            .AddDevToolsMonitors()
            .AddTestAttachmentHandlers()
            .AddSeleniumObjectInitializer()
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
    public void SetUp()
    {
        ServiceProvider.GetService<ISeleniumNetworkMonitor>().StartMonitoring();
        ServiceProvider.GetService<ISeleniumConsoleLogMonitor>().StartMonitoring();
    }

    [TearDown]
    public void TearDown()
    {
        var networkMonitor = ServiceProvider.GetService<ISeleniumNetworkMonitor>();
        var consoleLogMonitor = ServiceProvider.GetService<ISeleniumConsoleLogMonitor>();
        networkMonitor.StopMonitoring();
        consoleLogMonitor.StopMonitoring();
        GetTestAttachmentHandlers().ForEach(x => x.Execute(TestContext.CurrentContext));
        networkMonitor.ClearNetworkData();
        consoleLogMonitor.ClearLogMessages();
    }

    protected abstract void InitializePageObjects();

    private IList<ITestAttachmentHandler> GetTestAttachmentHandlers() => new List<ITestAttachmentHandler>()
    {
        ServiceProvider.GetService<IJavaScriptLogsTestAttachmentHandler>(),
        ServiceProvider.GetService<INetworkRequestLogsTestAttachmentHandler>(),
        ServiceProvider.GetService<ISeleniumScreenshotTestAttachmentHandler>()
    };
}