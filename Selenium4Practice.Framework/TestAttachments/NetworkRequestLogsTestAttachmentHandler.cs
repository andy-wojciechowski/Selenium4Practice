using NUnit.Framework;
using Selenium4Practice.Framework.NetworkMonitoring;
using Selenium4Practice.Framework.TestAttachments.Interfaces;

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
    }
}
