using System.Collections.Generic;

namespace Selenium4Practice.Framework.DevToolsMonitors.Interfaces;

public interface ISeleniumConsoleLogMonitor
{
    IList<string> LogMessages { get; }
    void StartMonitoring();
    void StopMonitoring();
    void ClearLogMessages();
}