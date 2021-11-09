using Selenium4Practice.Framework.Extensions;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Selenium4Practice.Framework.Grid
{
    public static class SeleniumGridManager
    {
        private static readonly string JavaProcessName = "java";

        public static void EnsureGridIsStarted(string gridJarPath, string seleniumServerUrl)
        {
            var gridJarFileName = Path.GetFileName(gridJarPath);
            var gridProcess = Process.GetProcessesByName(JavaProcessName).FirstOrDefault(x => x.StartInfo.Arguments.Contains(gridJarFileName));
            if (gridProcess != null)
            {
                var seleniumGridSessionIds = SeleniumGridUtilities.GetCurrentSeleniumGridSessionIds(seleniumServerUrl);
                seleniumGridSessionIds.ForEach(x => SeleniumGridUtilities.DeleteSeleniumGridSession(seleniumServerUrl, x));
            }
            else 
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