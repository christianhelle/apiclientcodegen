using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Generators
{
    public interface IProcessLauncher
    {
        void Start(
            string command,
            string arguments,
            string? workingDirectory = null);

        void Start(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string? workingDirectory = null);
    }

    [ExcludeFromCodeCoverage]
    public class ProcessLauncher : IProcessLauncher
    {
        private static readonly object SyncLock = new();

        public void Start(
            string command,
            string arguments,
            string? workingDirectory = null)
            => Start(
                command,
                arguments,
                o => Logger.Instance.WriteLine(o),
                e => Logger.Instance.WriteLine(e),
                workingDirectory);

        public void Start(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string? workingDirectory = null)
        {
            Logger.Instance.WriteLine("Executing:");
            Logger.Instance.WriteLine($"{command} {arguments}");

            if (!string.IsNullOrWhiteSpace(workingDirectory)) 
                Logger.Instance.WriteLine($"Working directory: {workingDirectory}");

            if (command.Contains("npm"))
            {
                lock (SyncLock)
                {
                    StartWithRetryPolicy(
                        command,
                        arguments,
                        onOutputData,
                        onErrorData,
                        workingDirectory);
                }
            }
            else
            {
                StartInternal(
                    command,
                    arguments,
                    onOutputData,
                    onErrorData,
                    workingDirectory);
            }
        }

        private static void StartWithRetryPolicy(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string? workingDirectory)
        {
            StartInternal(
                command,
                arguments,
                onOutputData,
                onErrorData,
                workingDirectory);
        }

        private static void StartInternal(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string? workingDirectory = null)
        {
            var processInfo = new ProcessStartInfo(command, arguments);
            using var process = new Process { StartInfo = processInfo };
            var outputData = new StringBuilder();
            process.OutputDataReceived += (s, e) =>
            {
                outputData.AppendLine(e.Data);
                onOutputData?.Invoke(e.Data);
            };

            var errorData = new StringBuilder();
            process.ErrorDataReceived += (s, e) =>
            {
                errorData.AppendLine(e.Data);
                onErrorData?.Invoke(e.Data);
            };

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

            var output = outputData.ToString();
            if (process.ExitCode != 0 && !output.Contains("Done."))
                throw new ProcessLaunchException(
                    command,
                    arguments,
                    workingDirectory,
                    output,
                    errorData.ToString());
        }
    }
}