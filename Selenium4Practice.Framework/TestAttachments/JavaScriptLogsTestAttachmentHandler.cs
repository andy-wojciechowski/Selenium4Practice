using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
        if (context.Result.Outcome == ResultState.Failure || context.Result.Outcome == ResultState.Error)
        {
            var jsErrors = _webDriver.Manage().Logs.GetLog(LogType.Browser).Where(x => x.Level == LogLevel.Severe).ToList();
            if (jsErrors.Any())
            {
                var logFileName = $"JS Errors {context.Test.MethodName} {Guid.NewGuid()}.txt";
                var log = new LoggerConfiguration().WriteTo.File(logFileName).CreateLogger();
                jsErrors.ForEach(x => log.Information(x.Message));
                var fullFilePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{logFileName}";
                TestContext.AddTestAttachment(fullFilePath);
            }
        }
    }
}
