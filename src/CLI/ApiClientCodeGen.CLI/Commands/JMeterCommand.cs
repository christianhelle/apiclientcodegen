using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands
{
    [Command("jmeter", Description = "Generate Apache JMeter test plans")]
    public class JMeterCommand
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IJMeterCodeGeneratorFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        private string? outputPath;

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

        [Required]
        [FileExists]
        [Argument(0, "swaggerFile", "Path to the Swagger / Open API specification file")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public string SwaggerFile { get; set; } = null!;

        [Argument(1, "outputPath", "Output folder to write the generated code to")]
        public string OutputPath
        {
            get => outputPath ?? "JMeter";
            set => outputPath = value;
        }

        [Option(ShortName = "nl", LongName = "no-logging", Description = "Disables Analytics and Error Reporting")]
        public bool SkipLogging { get; set; }

        public int OnExecute()
        {
            if (!SkipLogging)
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
                .Create(SwaggerFile, OutputPath, options, processLauncher, dependencyInstaller)
                .GenerateCode(progressReporter);

            if (!Directory.Exists(OutputPath))
            {
                OutputPath = Path.Combine(Path.GetDirectoryName(SwaggerFile)!, OutputPath);
            }

            var directoryInfo = new DirectoryInfo(OutputPath);
            var fileCount = directoryInfo.GetFiles().Length;
            if (fileCount != 0)
            {
                console.WriteLine($"Output folder name: {OutputPath}");
                console.WriteLine($"Output files: {fileCount}");
                console.WriteSignature();
            }
            else
            {
                const string errorMessage = "ERROR!! Output folder is empty :(";
                console.WriteLine(errorMessage);
                console.WriteLine(string.Empty);

                if (!SkipLogging)
                    Logger.Instance.TrackError(new Exception(errorMessage));
            }

            return fileCount != 0 ? ResultCodes.Success : ResultCodes.Error;
        }
    }
}