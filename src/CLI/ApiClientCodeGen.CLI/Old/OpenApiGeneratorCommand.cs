using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.OpenApiGenerator;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class OpenApiGeneratorSettings : BaseCommandSettings
    {
        [CommandArgument(0, "<SWAGGER_FILE>")]
        [Description("Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; } = null!;

        [CommandArgument(1, "<GENERATOR>")]
        [Description("Language/generator to use")]
        public string Generator { get; set; } = null!;

        [CommandArgument(2, "[OUTPUT_PATH]")]
        [Description("Output folder to write the generated code to")]
        public string OutputPath { get; set; } = "Generated";
    }

    public class OpenApiGeneratorCommand : AsyncCommand<OpenApiGeneratorSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly IOpenApiGeneratorFactory factory;
        private readonly IOpenApiGeneratorOptions options;

        public OpenApiGeneratorCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            IOpenApiGeneratorFactory factory,
            IOpenApiGeneratorOptions options)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, OpenApiGeneratorSettings settings)
        {
            AnsiConsole.MarkupLine($"[bold green]🚀 Generating {settings.Generator} code using OpenAPI Generator[/]");

            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage("OpenApiGenerator", "CLI");
            }
            else
            {
                Logger.Instance.Disable();
                console.WriteMarkup("[yellow]NOTE: Feature usage tracking and error Logging is disabled[/]");
                console.WriteLine("");
            }

            var generator = factory.Create(
                settings.SwaggerFile,
                settings.Generator,
                settings.OutputPath,
                options);

            await Task.Run(() => generator.GenerateCode(progressReporter));

            console.WriteMarkup($"[green]✅ {settings.Generator} code generated in:[/] {settings.OutputPath}");
            console.WriteLine("");
            console.WriteSignature();

            return ResultCodes.Success;
        }
    }
}