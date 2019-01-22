using System;
using System.IO;
using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode();
    }

    public abstract class CodeGenerator : ICodeGenerator
    {
        protected readonly string swaggerFile;
        protected readonly string defaultNamespace;

        protected CodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            var path = Path.GetDirectoryName(swaggerFile);
            var outputFile = Path.Combine(path, "TempApiClient.cs");

            var command = GetCommand();
            var arguments = GetArguments(outputFile);

            StartProcess(command, arguments);
            return ReadThenDelete(outputFile);
        }

        protected abstract string GetArguments(string outputFile);
        protected abstract string GetCommand();

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
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}
