using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium4Practice.Framework.TestAttachments.Interfaces;

public interface ITestAttachmentHandler
{
    void Execute(TestContext context, IWebDriver webDriver);
}