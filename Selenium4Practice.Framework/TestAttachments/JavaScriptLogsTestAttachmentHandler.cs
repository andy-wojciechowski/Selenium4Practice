using NUnit.Framework;
using OpenQA.Selenium;
using Selenium4Practice.Framework.TestAttachments.Interfaces;

namespace Selenium4Practice.Framework.TestAttachments;

public class JavaScriptLogsTestAttachmentHandler : IJavaScriptLogsTestAttachmentHandler
{
    private readonly IWebDriver _webDriver;

    public JavaScriptLogsTestAttachmentHandler(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void Execute(TestContext context)
    {
        throw new System.NotImplementedException();
    }
}
