using OpenQA.Selenium;
using Selenium4Practice.Framework.Extensions;
using System.Collections.Generic;

namespace Selenium4Practice.Framework.NetworkMonitoring;

public class SeleniumNetworkMonitor : ISeleniumNetworkMonitor
{
    private readonly IWebDriver _webDriver;
    private bool AreEventHandlersSubscribed { get; }
    public IDictionary<string, NetworkRequestSentEventArgs> NetworkRequests { get; }
    public IDictionary<string, NetworkResponseReceivedEventArgs> NetworkResponses { get; }

    public SeleniumNetworkMonitor(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        NetworkRequests = new Dictionary<string, NetworkRequestSentEventArgs>();
        NetworkResponses = new Dictionary<string, NetworkResponseReceivedEventArgs>();
        AreEventHandlersSubscribed = false;
    }

    public void StartMonitoring()
    {
        if (_webDriver.HasDevToolsCapability())
        {
            if (!AreEventHandlersSubscribed)
            {
                _webDriver.Manage().Network.NetworkRequestSent += RequestRecievedHandler;
                _webDriver.Manage().Network.NetworkResponseReceived += ResponseRecievedHandler;
            }
            _webDriver.Manage().Network.StartMonitoring().GetAwaiter().GetResult();
        }
    }

    public void StopMonitoring()
    {
        if (_webDriver.HasDevToolsCapability())
        {
            _webDriver.Manage().Network.StopMonitoring().GetAwaiter().GetResult();
            ClearNetworkData();
        }
    }

    public void ClearNetworkData()
    {
        NetworkRequests.Clear();
        NetworkResponses.Clear();
    }

    public void RequestRecievedHandler(object _, NetworkRequestSentEventArgs requestEventArgs) => NetworkRequests.Add(requestEventArgs.RequestId, requestEventArgs);

    public void ResponseRecievedHandler(object _, NetworkResponseReceivedEventArgs responseEventArgs) => NetworkResponses.Add(responseEventArgs.RequestId, responseEventArgs);
}