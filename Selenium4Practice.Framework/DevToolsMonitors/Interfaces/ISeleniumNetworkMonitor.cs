using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium4Practice.Framework.DevToolsMonitors.Interfaces;

public interface ISeleniumNetworkMonitor
{
    IDictionary<string, NetworkRequestSentEventArgs> NetworkRequests { get; }
    IDictionary<string, NetworkResponseReceivedEventArgs> NetworkResponses { get; }
    void StartMonitoring();
    void StopMonitoring();
    void ClearNetworkData();
    void RequestRecievedHandler(object sender, NetworkRequestSentEventArgs requestEventArgs);
    void ResponseRecievedHandler(object sender, NetworkResponseReceivedEventArgs responseEventArgs);
}
