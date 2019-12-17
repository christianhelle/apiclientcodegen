using System;
using System.Diagnostics;
using System.Text;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public interface IProcessLauncher
    {
        void Start(string command, string arguments);
    }

    public class ProcessLauncher : IProcessLauncher
    {
        public void Start(string command, string arguments)
            => Start(command, arguments, o => Trace.WriteLine(o), e => Trace.WriteLine(e));

        public void Start(string command, string arguments, Action<string> onOutputData, Action<string> onErrorData)
        {
            Trace.WriteLine("Executing:");
            Trace.WriteLine($"{command} {arguments}");

            var processInfo = new ProcessStartInfo(command, arguments);
            using (var process = new Process { StartInfo = processInfo })
            {
                process.OutputDataReceived += (s, e) => onOutputData?.Invoke(e.Data);
                process.ErrorDataReceived += (s, e) => onErrorData?.Invoke(e.Data);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.ExitCode != 0)
                    throw new InvalidOperationException($"{command} failed");
            }
        }
    }
}