using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium4Practice.Framework.NetworkMonitoring;

public class SeleniumNetworkMonitor : ISeleniumNetworkMonitor
{
    private bool AreEventHandlersSubscribed { get; }
    public IDictionary<string, NetworkRequestSentEventArgs> NetworkRequests { get; }
    public IDictionary<string, NetworkResponseReceivedEventArgs> NetworkResponses { get; }

    public SeleniumNetworkMonitor()
    {
        NetworkResponses = new Dictionary<string, NetworkResponseReceivedEventArgs>();
        NetworkResponses = new Dictionary<string, NetworkResponseReceivedEventArgs>();
        AreEventHandlersSubscribed = false;
    }

    public void StartMonitoring(IWebDriver webDriver)
    {
        if (!AreEventHandlersSubscribed)
        {
            webDriver.Manage().Network.NetworkRequestSent += RequestRecievedHandler;
            webDriver.Manage().Network.NetworkResponseReceived += ResponseRecievedHandler;
        }
        webDriver.Manage().Network.StartMonitoring().GetAwaiter().GetResult();
    }

    public void StopMonitoring(IWebDriver webDriver)
    {
        webDriver.Manage().Network.StopMonitoring().GetAwaiter().GetResult();
        ClearNetworkData();
    }

    public void ClearNetworkData()
    {
        NetworkRequests.Clear();
        NetworkResponses.Clear();
    }

    public void RequestRecievedHandler(object _, NetworkRequestSentEventArgs requestEventArgs) => NetworkRequests.Add(requestEventArgs.RequestId, requestEventArgs);

    public void ResponseRecievedHandler(object _, NetworkResponseReceivedEventArgs responseEventArgs) => NetworkResponses.Add(responseEventArgs.RequestId, responseEventArgs);
}