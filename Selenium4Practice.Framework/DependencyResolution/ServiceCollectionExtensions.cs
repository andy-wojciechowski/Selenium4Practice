using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System;
using Selenium4Practice.Framework.Configuration;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Framework.WebDriver;

namespace Selenium4Practice.Framework.DependencyResolution;

public static class ServiceCollectionExtensions
{
    private static readonly int DefaultTimeout = 30;

    public static IServiceCollection AddSeleniumObjectsContainingTypes(this IServiceCollection services, SeleniumObjectConfiguration config, params Type[] assemblyMarkerTypes)
    {
        var assemblies = assemblyMarkerTypes.Select(x => x.Assembly).ToArray();
        return services.AddSeleniumObjectsInAssemblies(config, assemblies);
    }

    public static IServiceCollection AddSeleniumObjectsInAssemblies(this IServiceCollection services, SeleniumObjectConfiguration config, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var seleniumObjectTypes = assembly.DefinedTypes.Where(x => typeof(ISeleniumObject).IsAssignableFrom(x) &&
                                                                  !x.IsAbstract && !x.IsInterface).ToList();
            foreach (var type in seleniumObjectTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(type, serviceProvider =>
                {
                    var webDriver = serviceProvider.GetRequiredService<IWebDriver>();
                    var timeoutAttribute = type.GetFirstAttributeOfType<TimeoutAttribute>();
                    var timeout = timeoutAttribute != null ? timeoutAttribute.Timeout : DefaultTimeout;
                    var instance = Activator.CreateInstance(type);
                    var seleniumObjectInstance = (ISeleniumObject)instance;
                    seleniumObjectInstance.WebDriver = webDriver;
                    seleniumObjectInstance.WebDriverWait = new WebDriverWait(seleniumObjectInstance.WebDriver, TimeSpan.FromSeconds(timeout));
                    if (typeof(IPage).IsAssignableFrom(type))
                    {
                        var pageUrlAttribute = type.GetFirstAttributeOfType<PageUrlAttribute>();
                        if (pageUrlAttribute == null) throw new Exception("A page url attribute is required for page objects");
                        ((IPage)seleniumObjectInstance).BaseUrl = config.PageBaseUrl;
                        ((IPage)seleniumObjectInstance).PageUrl = pageUrlAttribute.PageUrl;
                    }
                    return seleniumObjectInstance;
                }, ServiceLifetime.Transient);
                services.Add(serviceDescriptor);
            }
        }
        return services;
    }

    public static IServiceCollection AddWebDriver(this IServiceCollection services, Browser browser, string seleniumServerUrl)
    {
        services.AddSingleton(typeof(IWebDriver), _ =>
        {
            var driver = DriverFactory.CreateWebDriver(browser, seleniumServerUrl, true);
            driver.Manage().Window.Maximize();
            return driver;
        });
        return services;
    }
}