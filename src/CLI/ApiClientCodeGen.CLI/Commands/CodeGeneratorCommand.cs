using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public abstract class CodeGeneratorCommand<T> : Command<T> where T : CodeGeneratorCommand<T>.Settings
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;

        protected CodeGeneratorCommand(IConsoleOutput console, IProgressReporter? progressReporter)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<swaggerFile>")]
            [Description("Path to the Swagger / Open API specification file")]
            public string SwaggerFile { get; set; } = null!;

            [CommandArgument(1, "[namespace]")]
            [Description("Default namespace to in the generated code")]
            public string DefaultNamespace { get; set; } = "GeneratedCode";

            [CommandArgument(2, "[outputFile]")]
            [Description("Output filename to write the generated code to. Default is the swaggerFile .cs")]
            public string? OutputFile { get; set; }

            [CommandOption("--no-logging")]
            [Description("Disables Analytics and Error Reporting")]
            public bool SkipLogging { get; set; }

            public string GetOutputFile()
            {
                return OutputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".cs";
            }
        }

        public override int Execute(CommandContext context, T settings, CancellationToken cancellationToken)
        {
            var codeGeneratorName = GetType().Name.Replace("Command", string.Empty);
            if (!settings.SkipLogging)
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

            var generator = CreateGenerator(settings);
            var code = generator.GenerateCode(progressReporter);
            if (string.IsNullOrWhiteSpace(code))
            {
                console.WriteSignature();
                return ResultCodes.Success;
            }

            var outputFile = settings.GetOutputFile();
            File.WriteAllText(outputFile, code);

            var fileInfo = new FileInfo(outputFile);
            LogOutput(fileInfo);

            return fileInfo.Length != 0 ? ResultCodes.Success : ResultCodes.Error;
        }

        [ExcludeFromCodeCoverage]
        private void LogOutput(FileInfo fileInfo)
        {
            if (fileInfo.Length != 0)
            {
                console.WriteLine($"Output file name: {fileInfo.Name}");
                console.WriteLine($"Output file size: {fileInfo.Length}");
                console.WriteSignature();
            }
            else
            {
                const string errorMessage = "ERROR!! Output file is empty :(";
                console.WriteLine(errorMessage);
                console.WriteLine(string.Empty);

                Logger.Instance.TrackError(new Exception(errorMessage));
            }
        }

        public abstract ICodeGenerator CreateGenerator(T settings);
    }
}