using Microsoft.Extensions.DependencyInjection;
using System;

namespace Selenium4Practice.Framework.DependencyResolution;

public static class ServiceProviderExtensions
{
    public static void Dispose(this IServiceProvider serviceProvider) => ((ServiceProvider)serviceProvider).Dispose();
}
