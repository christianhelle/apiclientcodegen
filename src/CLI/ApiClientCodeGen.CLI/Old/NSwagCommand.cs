using System;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.NSwag;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class NSwagCommand : AsyncCommand<CodeGeneratorSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly INSwagCodeGeneratorFactory factory;
        private readonly INSwagOptions options;

        public NSwagCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            INSwagCodeGeneratorFactory factory,
            INSwagOptions options)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, CodeGeneratorSettings settings)
        {
            AnsiConsole.MarkupLine("[bold green]🚀 Generating C# code using NSwag[/]");
            
            var outputFile = settings.OutputFile ?? Path.GetFileNameWithoutExtension(settings.SwaggerFile) + ".cs";

            var codeGeneratorName = "NSwag";
            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage(codeGeneratorName, "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteMarkup("[yellow]NOTE: Feature usage tracking and error Logging is disabled[/]");
                console.WriteLine("");
            }

            var generator = factory.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                options);

            var code = await Task.Run(() => generator.GenerateCode(progressReporter));
            
            if (string.IsNullOrWhiteSpace(code))
            {
                console.WriteSignature();
                return ResultCodes.Success;
            }

            File.WriteAllText(outputFile, code);

            var fileInfo = new FileInfo(outputFile);
            LogOutput(fileInfo, outputFile, settings.SkipLogging);

            return fileInfo.Length != 0 ? ResultCodes.Success : ResultCodes.Error;
        }

        private void LogOutput(FileInfo fileInfo, string outputFile, bool skipLogging)
        {
            if (fileInfo.Length != 0)
            {
                console.WriteMarkup($"[green]✅ Output file name:[/] {outputFile}");
                console.WriteLine("");
                console.WriteMarkup($"[green]📊 Output file size:[/] {fileInfo.Length} bytes");
                console.WriteLine("");
                console.WriteSignature();
            }
            else
            {
                const string errorMessage = "ERROR!! Output file is empty :(";
                console.WriteMarkup($"[red]{errorMessage}[/]");
                console.WriteLine("");

                if (!skipLogging)
                    Logger.Instance.TrackError(new Exception(errorMessage));
            }
        }
    }
}