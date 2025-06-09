using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class KiotaSettings : CodeGeneratorSettings
    {
        [CommandOption("-m|--generate-multiple-files")]
        [Description("Set this to TRUE to generate multiple files (default: FALSE)")]
        public bool GenerateMultipleFiles { get; set; }
    }

    public class KiotaCommand : AsyncCommand<KiotaSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public KiotaCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, KiotaSettings settings)
        {
            AnsiConsole.MarkupLine("[bold green]🚀 Generating C# code using Kiota[/]");
            
            var outputFile = settings.OutputFile ?? Path.GetFileNameWithoutExtension(settings.SwaggerFile) + ".cs";

            var codeGeneratorName = "Kiota";
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

            var options = new KiotaOptions { GenerateMultipleFiles = settings.GenerateMultipleFiles };
            var generator = new KiotaCodeGenerator(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                processLauncher,
                dependencyInstaller,
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