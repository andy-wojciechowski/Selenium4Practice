using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;

namespace Selenium4Practice.Framework.Grid
{
    public static class SeleniumGridManager
    {
        private static readonly string JavaProcessName = "java";
        private static readonly int SeleniumServerPort = 4444;

        public static void EnsureGridIsStarted(string gridJarPath)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var isSeleniumPortInUse = ipProperties.GetActiveTcpListeners().Any(x => x.Port == SeleniumServerPort);
            if (!isSeleniumPortInUse)
            {
                var newGridProcess = new Process();
                newGridProcess.StartInfo.UseShellExecute = false;
                newGridProcess.StartInfo.CreateNoWindow = true;
                newGridProcess.StartInfo.FileName = JavaProcessName;
                newGridProcess.StartInfo.Arguments = $"-jar {gridJarPath} standalone";
                newGridProcess.Start();
            }
        }
    }
}