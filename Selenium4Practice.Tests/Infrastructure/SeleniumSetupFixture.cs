using NUnit.Framework;
using Selenium4Practice.Tests.Infrastructure;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

[SetUpFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "NUnit setup fixtures for assemblies are not declared in namespaces.")]
public class SeleniumSetupFixture
{
    private const string VersionText = "<version>";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var webDriverManager = new DriverManager();
        webDriverManager.SetUpDriver(GetDriveUrl(new ChromeConfig()), ConfigSettings.SeleniumDriversPath);
        webDriverManager.SetUpDriver(GetDriveUrl(new FirefoxConfig()), ConfigSettings.SeleniumDriversPath);
        webDriverManager.SetUpDriver(GetDriveUrl(new EdgeConfig()), ConfigSettings.SeleniumDriversPath);
    }

    private string GetDriveUrl(IDriverConfig driverConfig) => driverConfig.GetUrl32().Replace(VersionText, driverConfig.GetLatestVersion());
}
