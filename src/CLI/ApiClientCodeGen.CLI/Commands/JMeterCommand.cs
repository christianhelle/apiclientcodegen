using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class JMeterCommand : Command<JMeterCommand.Settings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IJMeterCodeGeneratorFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<swaggerFile>")]
            [Description("Path to the Swagger / Open API specification file")]
            public string SwaggerFile { get; set; } = null!;

            [CommandArgument(1, "[outputPath]")]
            [Description("Output folder to write the generated code to")]
            public string OutputPath { get; set; } = "JMeter";

            [CommandOption("--no-logging")]
            [Description("Disables Analytics and Error Reporting")]
            public bool SkipLogging { get; set; }
        }

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
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage(
                    "JMeter",
                    "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteLine("NOTE: Feature usage tracking and error Logging is disabled");
            }

            factory
                .Create(settings.SwaggerFile, settings.OutputPath, options, processLauncher, dependencyInstaller)
                .GenerateCode(progressReporter);

            var outputPath = settings.OutputPath;
            if (!Directory.Exists(outputPath))
            {
                var swaggerDirectory = Path.GetDirectoryName(settings.SwaggerFile) ?? Directory.GetCurrentDirectory();
                outputPath = Path.Combine(swaggerDirectory, settings.OutputPath);
            }

            var directoryInfo = new DirectoryInfo(outputPath);
            var fileCount = directoryInfo.GetFiles().Length;
            if (fileCount != 0)
            {
                console.WriteLine($"Output folder name: {outputPath}");
                console.WriteLine($"Output files: {fileCount}");
                console.WriteSignature();
            }
            else
            {
                const string errorMessage = "ERROR!! Output folder is empty :(";
                console.WriteLine(errorMessage);
                console.WriteLine(string.Empty);

                if (!settings.SkipLogging)
                    Logger.Instance.TrackError(new Exception(errorMessage));
            }

            return fileCount != 0 ? ResultCodes.Success : ResultCodes.Error;
        }
    }
}