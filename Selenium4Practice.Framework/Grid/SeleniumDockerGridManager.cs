using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System.Linq;

namespace Selenium4Practice.Framework.Grid
{
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
        }
    }
}