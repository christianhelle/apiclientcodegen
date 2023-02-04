using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command("typescript", Description = "Generate TypeScript API clients")]
    public class TypeScriptCommand
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly ITypeScriptCodeGeneratorFactory factory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        private string? outputPath;

        public TypeScriptCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            ITypeScriptCodeGeneratorFactory factory,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.factory = factory;
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        [Required]
        [Argument(0, "generator", Description = "The tech stack to use for the generated client library")] 
        public OpenApiTypeScriptGenerator Generator { get; set; }

        [Required]
        [FileExists]
        [Argument(1, "swaggerFile", "Path to the Swagger / Open API specification file")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public string SwaggerFile { get; set; } = null!;

        [Argument(2, "outputPath", "Output folder to write the generated code to")]
        public string OutputPath
        {
            get => outputPath ?? "typescript-generated-code";
            set => outputPath = value;
        }

        [Option(ShortName = "nl", LongName = "no-logging", Description = "Disables Analytics and Error Reporting")]
        public bool SkipLogging { get; set; }

        public int OnExecute()
        {
            if (!SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage(
                    "TypeScript",
                    "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteLine("NOTE: Feature usage tracking and error Logging is disabled");
            }


            factory
                .Create(Generator, SwaggerFile, OutputPath, options, processLauncher, dependencyInstaller)
                .GenerateCode(progressReporter);

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