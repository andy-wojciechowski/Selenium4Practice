using FluentAssertions;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Tests.Infrastructure;
using Selenium4Practice.Pages;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Selenium4Practice.Tests.Docker;

public class BasicTest : SeleniumDockerBaseTest
{
    private HomePage HomePage { get; set; }

    public BasicTest(Browser browser) : base(browser)
    {
    }

    protected override void InitializePageObjects()
    {
        HomePage = ServiceProvider.GetService<HomePage>();
    }

    [Test]
    public void BasicTestMethod()
    {
        HomePage.Navigate();
        HomePage.IsAt().Should().BeTrue();
    }
}