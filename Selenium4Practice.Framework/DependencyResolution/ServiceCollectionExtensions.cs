using Microsoft.Extensions.DependencyInjection;
using Selenium4Practice.Framework.DevToolsMonitors;
using Selenium4Practice.Framework.DevToolsMonitors.Interfaces;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Framework.SeleniumObjects;
using Selenium4Practice.Framework.SeleniumObjects.Interfaces;
using Selenium4Practice.Framework.TestAttachments;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Selenium4Practice.Framework.WebDriver;
using System;
using System.Linq;
using System.Reflection;

namespace Selenium4Practice.Framework.DependencyResolution;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSeleniumObjectsContainingTypes(this IServiceCollection services, params Type[] assemblyMarkerTypes)
    {
        var assemblies = assemblyMarkerTypes.Select(x => x.Assembly).ToArray();
        return services.AddSeleniumObjectsInAssemblies(assemblies);
    }

    public static IServiceCollection AddSeleniumObjectsInAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var seleniumObjectTypes = assembly.DefinedTypes.Where(x => typeof(ISeleniumObject).IsAssignableFrom(x) &&
                                                                  !x.IsAbstract && !x.IsInterface).ToList();
            seleniumObjectTypes.ForEach(type => services.AddTransient(type));
        }
        return services;
    }

    public static IServiceCollection AddWebDriver(this IServiceCollection services, Browser browser, string seleniumServerUrl, bool setFirefoxBinary = false)
    {
        services.AddSingleton(_ =>
        {
            var driver = DriverFactory.CreateWebDriver(browser, seleniumServerUrl, setFirefoxBinary);
            driver.Manage().Window.Maximize();
            return driver;
        });
        return services;
    }

    public static IServiceCollection AddDevToolsMonitors(this IServiceCollection services)
    {
        services.AddSingleton<ISeleniumNetworkMonitor, SeleniumNetworkMonitor>();
        services.AddSingleton<ISeleniumConsoleLogMonitor, SeleniumConsoleLogMonitor>();
        return services;
    }

    public static IServiceCollection AddTestAttachmentHandlers(this IServiceCollection services)
    {
        services.AddTransient<IJavaScriptLogsTestAttachmentHandler, JavaScriptLogsTestAttachmentHandler>();
        services.AddTransient<INetworkRequestLogsTestAttachmentHandler, NetworkRequestLogsTestAttachmentHandler>();
        services.AddTransient<ISeleniumScreenshotTestAttachmentHandler, SeleniumScreenshotTestAttachmentHandler>();
        return services;
    }

    public static IServiceCollection AddSeleniumObjectInitializer(this IServiceCollection services)
    {
        services.AddTransient<ISeleniumObjectInitializer, SeleniumObjectInitializer>();
        return services;
    }
}