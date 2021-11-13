using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium4Practice.Framework.NetworkMonitoring;

public interface ISeleniumNetworkMonitor
{
    IDictionary<string, NetworkRequestSentEventArgs> NetworkRequests { get; }
    IDictionary<string, NetworkResponseReceivedEventArgs> NetworkResponses { get; }
    void StartMonitoring(IWebDriver webDriver);
    void StopMonitoring(IWebDriver webDriver);
    void ClearNetworkData();
    void RequestRecievedHandler(object sender, NetworkRequestSentEventArgs requestEventArgs);
    void ResponseRecievedHandler(object sender, NetworkResponseReceivedEventArgs responseEventArgs);
}
