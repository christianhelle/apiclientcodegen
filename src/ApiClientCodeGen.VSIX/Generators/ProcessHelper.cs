using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class ProcessHelper
    {
        public static void StartProcess(string command, string arguments)
        {
            var processInfo = new ProcessStartInfo(command, arguments);
            using (var process = new Process { StartInfo = processInfo })
            {
                process.OutputDataReceived += (s, e) => Trace.WriteLine(e.Data);
                process.ErrorDataReceived += (s, e) => Trace.WriteLine(e.Data);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}