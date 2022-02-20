using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

[SetUpFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "NUnit setup fixtures for assemblies are not declared in namespaces.")]
public class SeleniumSetupFixture
{
    private const string VersionText = "<version>";

    public static IConfiguration Config { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var webDriverManager = new DriverManager();
        webDriverManager.SetUpDriver(GetDriveUrl(new ChromeConfig()), Config["SeleniumDriversPath"]);
        webDriverManager.SetUpDriver(GetDriveUrl(new FirefoxConfig()), Config["SeleniumDriversPath"]);
        webDriverManager.SetUpDriver(GetDriveUrl(new EdgeConfig()), Config["SeleniumDriversPath"]);
    }

    private string GetDriveUrl(IDriverConfig driverConfig) => driverConfig.GetUrl32().Replace(VersionText, driverConfig.GetLatestVersion());
}
