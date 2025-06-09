using System;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class RefitterCommandSpectre : AsyncCommand<RefitterSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IRefitterCodeGeneratorFactory factory;
        private readonly IRefitterOptions options;

        public RefitterCommandSpectre(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IRefitterCodeGeneratorFactory factory,
            IRefitterOptions options)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, RefitterSettings settings)
        {
            AnsiConsole.MarkupLine("[bold green]🚀 Generating C# code using Refitter[/]");
            
            var swaggerFile = settings.SwaggerFile;
            var outputFile = settings.OutputFile ?? Path.GetFileNameWithoutExtension(swaggerFile) + ".cs";

            // If a settings file is specified, validate it exists
            if (!string.IsNullOrEmpty(settings.SettingsFile))
            {
                swaggerFile = settings.SettingsFile;
            }

            // Map settings to options
            options.GenerateContracts = !settings.SkipGenerateContracts;
            options.GenerateXmlDocCodeComments = !settings.SkipGenerateXmlDocCodeComments;
            options.ReturnIApiResponse = settings.ReturnIApiResponse;
            options.GenerateMultipleFiles = settings.MultipleFiles;

            var codeGeneratorName = "Refitter";
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
                swaggerFile,
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