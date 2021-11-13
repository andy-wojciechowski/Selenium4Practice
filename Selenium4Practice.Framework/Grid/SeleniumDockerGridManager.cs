using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System.Linq;
using Selenium4Practice.Framework.Extensions;

namespace Selenium4Practice.Framework.Grid;

public static class SeleniumDockerGridManager
{
    public static void EnsureGridIsStarted(string composePath, string seleniumGridContainerName)
    {
        var hosts = new Hosts().Discover();
        var nativeDockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        var seleniumGridContainer = nativeDockerHost.GetContainers().FirstOrDefault(x => x.Name == seleniumGridContainerName);
        if (seleniumGridContainer == null)
        {
            new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(composePath)
                .RemoveOrphans()
                .Build().Start();
        }
        else if (seleniumGridContainer.State == ServiceRunningState.Stopped)
        {
            seleniumGridContainer.Start();
            var nodeContainers = nativeDockerHost.GetContainers().Where(x => x.Image.Name.Contains("chrome") ||
                                                                             x.Image.Name.Contains("firefox") ||
                                                                             x.Image.Name.Contains("edge"));
            nodeContainers.ForEach(x => x.Start());
        }
    }
}