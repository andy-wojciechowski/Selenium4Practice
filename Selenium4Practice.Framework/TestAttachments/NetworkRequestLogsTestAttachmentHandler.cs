using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Selenium4Practice.Framework.NetworkMonitoring;
using Selenium4Practice.Framework.TestAttachments.Interfaces;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Selenium4Practice.Framework.TestAttachments;

public class NetworkRequestLogsTestAttachmentHandler : INetworkRequestLogsTestAttachmentHandler
{
    private readonly ISeleniumNetworkMonitor _seleniumNetworkMonitor;

    public NetworkRequestLogsTestAttachmentHandler(ISeleniumNetworkMonitor seleniumNetworkMonitor)
    {
        _seleniumNetworkMonitor = seleniumNetworkMonitor;
    }

    public void Execute(TestContext context)
    {
        if (context.Result.Outcome == ResultState.Failure || context.Result.Outcome == ResultState.Error)
        {
            var failedResponses = _seleniumNetworkMonitor.NetworkResponses.Where(x => x.Value.ResponseStatusCode != 200);
            if (failedResponses.Any())
            {
                var logFileName = $"Network Errors {context.Test.MethodName} {Guid.NewGuid()}.txt";
                var log = new LoggerConfiguration().WriteTo.File(logFileName).CreateLogger();
                foreach (var failedResponse in failedResponses)
                {
                    var requestMethod = _seleniumNetworkMonitor.NetworkRequests.Single(x => x.Key == failedResponse.Key).Value.RequestMethod;
                    log.Information($"{requestMethod} {failedResponse.Value.ResponseUrl} {failedResponse.Value.ResponseBody}");
                }
                var fullFilePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{logFileName}";
                TestContext.AddTestAttachment(fullFilePath);
            }
        }
    }
}
