using FluentAssertions;
using Selenium4Practice.Framework.Enums;
using Selenium4Practice.Pages;
using Selenium4Practice.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Selenium4Practice.Tests.Standard
{
    public class BasicTest : SeleniumBaseTest
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
}