using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands
{
    public abstract class CodeGeneratorCommand
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private string? outputFile;

        protected CodeGeneratorCommand(IConsoleOutput console, IProgressReporter? progressReporter)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
        }

        [FileExists]
        [Argument(0, "swaggerFile", "Path to the Swagger / Open API specification file")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public string SwaggerFile { get; set; } = null!;

        [Argument(1, "namespace", "Default namespace to in the generated code")]
        public string DefaultNamespace { get; set; } = "GeneratedCode";

        [Argument(2, "outputFile", "Output filename to write the generated code to. Default is the swaggerFile .cs")]
        public string OutputFile
        {
            get => outputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".cs";
            set => outputFile = value;
        }

        [Option(ShortName = "nl", LongName = "no-logging", Description = "Disables Analytics and Error Reporting")]
        public bool SkipLogging { get; set; }

        public int OnExecute()
        {
            var codeGeneratorName = this.GetCodeGeneratorName();
            if (!SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage(
                    codeGeneratorName,
                    "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteLine("NOTE: Feature usage tracking and error Logging is disabled");
            }

            var generator = CreateGenerator();
            var code = generator.GenerateCode(progressReporter);
            if (string.IsNullOrWhiteSpace(code))
            {
                console.WriteSignature();
                return ResultCodes.Success;
            }

            File.WriteAllText(OutputFile, code);

            var fileInfo = new FileInfo(OutputFile);
            LogOutput(fileInfo);

            return fileInfo.Length != 0 ? ResultCodes.Success : ResultCodes.Error;
        }

        [ExcludeFromCodeCoverage]
        private void LogOutput(FileInfo fileInfo)
        {
            if (fileInfo.Length != 0)
            {
                console.WriteLine($"Output file name: {OutputFile}");
                console.WriteLine($"Output file size: {fileInfo.Length}");
                console.WriteSignature();
            }
            else
            {
                const string errorMessage = "ERROR!! Output file is empty :(";
                console.WriteLine(errorMessage);
                console.WriteLine(string.Empty);

                if (!SkipLogging)
                    Logger.Instance.TrackError(new Exception(errorMessage));
            }
        }

        public abstract ICodeGenerator CreateGenerator();
    }
}