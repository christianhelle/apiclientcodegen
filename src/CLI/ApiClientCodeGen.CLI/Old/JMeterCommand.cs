using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class JMeterSettings : BaseCommandSettings
    {
        [CommandArgument(0, "<SWAGGER_FILE>")]
        [Description("Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; } = null!;

        [CommandArgument(1, "[OUTPUT_PATH]")]
        [Description("Output folder to write the generated code to")]
        public string OutputPath { get; set; } = "JMeter";
    }

    public class JMeterCommand : AsyncCommand<JMeterSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IJMeterCodeGeneratorFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public JMeterCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IJMeterCodeGeneratorFactory factory,
            IDependencyInstaller dependencyInstaller)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, JMeterSettings settings)
        {
            AnsiConsole.MarkupLine("[bold green]🚀 Generating JMeter test plans[/]");

            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage("JMeter", "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteMarkup("[yellow]NOTE: Feature usage tracking and error Logging is disabled[/]");
                console.WriteLine("");
            }

            var generator = factory.Create(
                settings.SwaggerFile,
                settings.OutputPath,
                options,
                processLauncher,
                dependencyInstaller);

            await Task.Run(() => generator.GenerateCode(progressReporter));

            console.WriteMarkup($"[green]✅ JMeter test plans generated in:[/] {settings.OutputPath}");
            console.WriteLine("");
            console.WriteSignature();

            return ResultCodes.Success;
        }
    }
}