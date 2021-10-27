using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System;
using Selenium4Practice.Framework.Interfaces;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.Attributes;
using OpenQA.Selenium;

namespace Selenium4Practice.FrameworkndencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeleniumObjectsContainingTypes(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            var assemblies = assemblyMarkerTypes.Select(x => x.Assembly).ToArray();
            return services.AddSeleniumObjectsInAssemblies(assemblies);
        }

        public static IServiceCollection AddSeleniumObjectsInAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach(var assembly in assemblies)
            {
                var seleniumObjectTypes = assembly.DefinedTypes.Where(x => x.IsAssignableFrom(typeof(ISeleniumObject)) &&
                                                                      !x.IsAbstract && !x.IsInterface).ToList();
                foreach (var type in seleniumObjectTypes)
                {
                    var serviceDescriptor = new ServiceDescriptor(type, serviceProvider => 
                    {
                        var webDriver = serviceProvider.GetRequiredService<IWebDriver>();
                        var timeout = type.GetFirstAttributeOfType<TimeoutAttribute>().Timeout;
                        var instance = Activator.CreateInstance(type);
                        var seleniumObjectInstance = instance as ISeleniumObject;
                        seleniumObjectInstance.WebDriver = webDriver;
                        seleniumObjectInstance.Timeout = timeout;
                        return seleniumObjectInstance;
                    }, ServiceLifetime.Transient);
                    services.Add(serviceDescriptor);
                }
            }
            return services;
        }
    }
}