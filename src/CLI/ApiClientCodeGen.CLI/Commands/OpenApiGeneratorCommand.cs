using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class OpenApiGeneratorCommandSettings : CommandSettings
    {
        [Required]
        [CommandArgument(0, "<generator>")]
        [Description(
            @"The tech stack to use for generating code.
See supported generators at https://openapi-generator.tech/docs/generators/")]
        public string Generator { get; set; } = null!;

        [Required]
        [CommandArgument(1, "<swaggerFile>")]
        [Description("Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; } = null!;

        [CommandArgument(2, "[outputPath]")]
        [Description("Output folder to write the generated code to")]
        public string? OutputPath { get; set; }

        [CommandOption("--no-logging")]
        [Description("Disables Analytics and Error Reporting")]
        public bool SkipLogging { get; set; }
    }

    public class OpenApiGeneratorCommand : Command<OpenApiGeneratorCommandSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly IOpenApiGeneratorFactory factory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public OpenApiGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter progressReporter,
            IGeneralOptions options,
            IOpenApiGeneratorFactory factory,
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

        public override int Execute(CommandContext context, OpenApiGeneratorCommandSettings settings, CancellationToken cancellationToken)
        {
            var outputPath = settings.OutputPath ?? $"{settings.Generator}-generated-code";

            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage(
                    $"openapi-generator {settings.Generator}",
                    "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteLine("NOTE: Feature usage tracking and error Logging is disabled");
            }

            factory
                .Create(settings.Generator, settings.SwaggerFile, outputPath, options, processLauncher,
                    dependencyInstaller)
                .GenerateCode(progressReporter);

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