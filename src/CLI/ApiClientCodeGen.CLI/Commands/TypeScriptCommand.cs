using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    public class TypeScriptCommand : Command<TypeScriptCommand.Settings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IGeneralOptions options;
        private readonly ITypeScriptCodeGeneratorFactory factory;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<generator>")]
            [Description("The tech stack to use for the generated client library")]
            public OpenApiTypeScriptGenerator Generator { get; set; }

            [CommandArgument(1, "<swaggerFile>")]
            [Description("Path to the Swagger / Open API specification file")]
            public string SwaggerFile { get; set; } = null!;

            [CommandArgument(2, "[outputPath]")]
            [Description("Output folder to write the generated code to")]
            public string OutputPath { get; set; } = "typescript-generated-code";

            [CommandOption("--no-logging")]
            [Description("Disables Analytics and Error Reporting")]
            public bool SkipLogging { get; set; }
        }

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

        public override int Execute(CommandContext context, Settings settings)
        {
            if (!settings.SkipLogging)
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
                .Create(settings.Generator, settings.SwaggerFile, settings.OutputPath, options, processLauncher,
                    dependencyInstaller)
                .GenerateCode(progressReporter);

            var directoryInfo = new DirectoryInfo(settings.OutputPath);
            var fileCount = directoryInfo.GetFiles().Length;
            if (fileCount != 0)
            {
                console.WriteLine($"Output folder name: {settings.OutputPath}");
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