using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Selenium4Practice.Framework.DevToolsMonitors.Interfaces;
using Selenium4Practice.Framework.Extensions;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Selenium4Practice.Framework.TestAttachments;

public class JavaScriptLogsTestAttachmentHandler : IJavaScriptLogsTestAttachmentHandler
{
    private readonly ISeleniumConsoleLogMonitor _consoleLogMonitor;

    public JavaScriptLogsTestAttachmentHandler(ISeleniumConsoleLogMonitor consoleLogMonitor)
    {
        _consoleLogMonitor = consoleLogMonitor;
    }

    public void Execute(TestContext context)
    {
        if (context.Result.Outcome == ResultState.Failure || context.Result.Outcome == ResultState.Error)
        {
            if (_consoleLogMonitor.LogMessages.Any())
            {
                var logFileName = $"JS_Messages_{context.Test.MethodName}_{Guid.NewGuid()}.txt";
                var log = new LoggerConfiguration().WriteTo.File(logFileName).CreateLogger();
                _consoleLogMonitor.LogMessages.ForEach(x => log.Information(x));
                var fullFilePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{logFileName}";
                TestContext.AddTestAttachment(fullFilePath);
            }
        }
    }
}
