using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Remote;
using Selenium4Practice.Framework.DevToolsMonitors.Interfaces;
using Selenium4Practice.Framework.Extensions;
using System.Collections.Generic;

namespace Selenium4Practice.Framework.DevToolsMonitors;

public class SeleniumConsoleLogMonitor : ISeleniumConsoleLogMonitor
{
    private readonly IWebDriver _webDriver;
    private bool IsEventHandlersSubscribed { get; }
    public IList<string> LogMessages { get; }

    public SeleniumConsoleLogMonitor(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        IsEventHandlersSubscribed = false;
        LogMessages = new List<string>();
    }

    public void StartMonitoring()
    {
        if (_webDriver.HasDevToolsCapability())
        {
            var remoteWebDriver = (RemoteWebDriver)_webDriver;
            var devToolsSession = remoteWebDriver.GetDevToolsSession();
            var logs = devToolsSession.Domains.Log;
            if (!IsEventHandlersSubscribed)
                logs.EntryAdded += LogEntryAddedEventHandler;
            logs.Enable().GetAwaiter().GetResult();
        }
    }

    public void StopMonitoring()
    {
        if (_webDriver.HasDevToolsCapability())
        {
            var remoteWebDriver = (RemoteWebDriver)_webDriver;
            var devToolsSession = remoteWebDriver.GetDevToolsSession();
            var logs = devToolsSession.Domains.Log;
            logs.Disable().GetAwaiter().GetResult();
        }
    }

    public void ClearLogMessages() => LogMessages.Clear();

    public void LogEntryAddedEventHandler(object _, EntryAddedEventArgs entryAddedEventArgs) => LogMessages.Add(entryAddedEventArgs.Entry.Message);
}