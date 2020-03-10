using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public interface IProcessLauncher
    {
        void Start(string command, string arguments);
        void Start(string command, string arguments, Action<string> onOutputData, Action<string> onErrorData);
    }

    [ExcludeFromCodeCoverage]
    public class ProcessLauncher : IProcessLauncher
    {
        private readonly string workingDirectory;
        private static readonly object syncLock = new object();

        public ProcessLauncher(string workingDirectory = null)
        {
            this.workingDirectory = workingDirectory;
        }

        public void Start(string command, string arguments)
            => Start(command, arguments, o => Trace.WriteLine(o), e => Trace.WriteLine(e));

        public void Start(string command, string arguments, Action<string> onOutputData, Action<string> onErrorData)
        {
            Trace.WriteLine("Executing:");
            Trace.WriteLine($"{command} {arguments}");

            lock (syncLock)
                StartInternal(command, arguments, onOutputData, onErrorData);
        }

        private void StartInternal(
            string command, 
            string arguments, 
            Action<string> onOutputData, 
            Action<string> onErrorData)
        {
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
                process.StartInfo.EnvironmentVariables["CORECLR_ENABLE_PROFILING"] = "0";
                process.StartInfo.EnvironmentVariables["COR_ENABLE_PROFILING"] = "0";

                if (workingDirectory != null)
                    process.StartInfo.WorkingDirectory = workingDirectory;

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