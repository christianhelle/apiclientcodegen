using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.VisualBasic
{
    public abstract class VisualBasicCodeGeneratorCommand<T> : Command<T> 
        where T : VisualBasicCodeGeneratorCommand<T>.Settings
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly ILanguageConverter converter;

        protected VisualBasicCodeGeneratorCommand(
            IConsoleOutput console, 
            IProgressReporter? progressReporter,
            ILanguageConverter converter)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<swaggerFile>")]
            [System.ComponentModel.Description("Path to the Swagger / Open API specification file")]
            public string SwaggerFile { get; set; } = null!;

            [CommandArgument(1, "[namespace]")]
            [System.ComponentModel.Description("Default namespace to in the generated code")]
            public string DefaultNamespace { get; set; } = "GeneratedCode";

            [CommandArgument(2, "[outputFile]")]
            [System.ComponentModel.Description("Output filename to write the generated code to. Default is the swaggerFile .vb")]
            public string? OutputFile { get; set; }

            [CommandOption("--no-logging")]
            [System.ComponentModel.Description("Disables Analytics and Error Reporting")]
            public bool SkipLogging { get; set; }

            public string GetOutputFile()
            {
                return OutputFile ?? Path.GetFileNameWithoutExtension(SwaggerFile) + ".vb";
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
            var csharpCode = generator.GenerateCode(progressReporter);
            if (string.IsNullOrWhiteSpace(csharpCode))
            {
                console.WriteSignature();
                return ResultCodes.Success;
            }

            // Convert C# to VB.NET
            console.WriteLine("Converting C# code to Visual Basic...");
            var vbCode = converter.ConvertAsync(csharpCode).GetAwaiter().GetResult();
            
            if (string.IsNullOrWhiteSpace(vbCode))
            {
                console.WriteLine("ERROR: Failed to convert C# code to Visual Basic");
                return ResultCodes.Error;
            }

            var outputFile = settings.GetOutputFile();
            File.WriteAllText(outputFile, vbCode);

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
