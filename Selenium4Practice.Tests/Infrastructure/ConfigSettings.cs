using Microsoft.Extensions.Configuration;

namespace Selenium4Practice.Tests.Infrastructure
{
    public static class ConfigSettings
    {
        private static IConfiguration Config { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static string BaseUrl => Config["BaseUrl"];
        public static string SeleniumServerUrl => Config["SeleniumServerUrl"];
        public static string SeleniumGridJarPath => Config["SeleniumGridJarPath"];
        public static string DockerComposePath => Config["DockerComposePath"];
        public static string SeleniumGridContainerName => Config["SeleniumGridContainerName"];
    }
}