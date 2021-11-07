using System.Diagnostics;
using System.IO;
using System.Linq;
using System;

namespace Selenium4Practice.Framework.Grid
{
    public static class SeleniumGridManager
    {
        private static readonly string JavaProcessName = "java";
        private static readonly string ChromeDriverProcessName = "chromedriver.exe";
        private static readonly string FirefoxDriverProcessName = "geckodriver.exe";
        private static readonly string EdgeDriverProcessName = "msedgedriver.exe";

        public static void EnsureGridIsStarted(string gridJarPath)
        {
            var gridJarFileName = Path.GetFileName(gridJarPath);
            var gridProcess = Process.GetProcessesByName(JavaProcessName).FirstOrDefault(x => x.StartInfo.Arguments.Contains(gridJarFileName));
            if (gridProcess != null)
            {
                var driverProcesses = Process.GetProcesses().Where(x => x.ProcessName == ChromeDriverProcessName || x.ProcessName == FirefoxDriverProcessName || x.ProcessName == EdgeDriverProcessName).ToList();
                var gridRuntime = DateTime.Now - gridProcess.StartTime;
                // If there are stale driver processes on the grid and the grid has been running for at least an hour
                // Kill all of the Selenium processes and restart the grid
                if (driverProcesses.Any() && gridRuntime.Hours >= 1)
                {
                    driverProcesses.ForEach(x => x.Kill());
                    gridProcess.Kill();
                    StartGridProcess(gridJarPath);
                }
            }
            else 
            {
                StartGridProcess(gridJarPath);
            }
        }

        private static void StartGridProcess(string gridJarPath)
        {
            var gridProcess = new Process();
            gridProcess.StartInfo.UseShellExecute = false;
            gridProcess.StartInfo.CreateNoWindow = true;
            gridProcess.StartInfo.FileName = JavaProcessName;
            gridProcess.StartInfo.Arguments = $"-jar {gridJarPath} standalone";
            gridProcess.Start();
        }
    }
}