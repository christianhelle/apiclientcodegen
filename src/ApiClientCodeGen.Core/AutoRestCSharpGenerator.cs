using System;
using System.Diagnostics;
using System.IO;

namespace ApiClientCodeGen
{
    public class AutoRestCSharpGenerator : ICodeGenerator
    {
        private readonly string swaggerFile;
        private readonly string defaultNamespace;

        public AutoRestCSharpGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            var path = Path.GetDirectoryName(swaggerFile);
            var outputFile = Path.Combine(path, "TempApiClient.cs");

            var autorestCmd = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "npm\\autorest.cmd");

            if (!File.Exists(autorestCmd))
                throw new NotInstalledException("AutoRest not installed. Please install this through NPM");

            var arguments =
                $"--csharp " +
                $"--input-file=\"{swaggerFile}\" " +
                $"--output-file=\"{outputFile}\" " +
                $"--namespace=\"{defaultNamespace}\" " +
                $"--add-credentials";

            StartProcess(autorestCmd, arguments);
            return ReadThenDelete(outputFile);
        }

        private static string ReadThenDelete(string outputFile)
        {
            try
            {
                return File.ReadAllText(outputFile);
            }
            finally
            {
                File.Delete(outputFile);
            }
        }

        private static void StartProcess(string autorestCmd, string arguments)
        {
            var processInfo = new ProcessStartInfo(autorestCmd, arguments);
            using (var process = new Process { StartInfo = processInfo })
            {
                process.OutputDataReceived += (s, e) => Trace.WriteLine(e.Data);
                process.ErrorDataReceived += (s, e) => Trace.WriteLine(e.Data);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}
