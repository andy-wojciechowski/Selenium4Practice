using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace Selenium4Practice.Framework.TestAttachments;

public class SeleniumScreenshotTestAttachmentHandler : ISeleniumScreenshotTestAttachmentHandler
{
    public void Execute(TestContext context, IWebDriver webDriver)
    {
        if (context.Result.Outcome == ResultState.Failure || context.Result.Outcome == ResultState.Error)
        {
            var screenshot = (ITakesScreenshot)webDriver;
            var filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{context.Test.MethodName} {Guid.NewGuid()}.png";
            screenshot.GetScreenshot().SaveAsFile(filePath, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(filePath);
        }
    }
}
