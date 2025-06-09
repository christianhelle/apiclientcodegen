using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.CLI.Extensions;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class TypeScriptSettings : BaseCommandSettings
    {
        [CommandArgument(0, "<SWAGGER_FILE>")]
        [Description("Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; } = null!;

        [CommandArgument(1, "[OUTPUT_PATH]")]
        [Description("Output folder to write the generated code to")]
        public string OutputPath { get; set; } = "TypeScript";
    }

    public class TypeScriptCommand : AsyncCommand<TypeScriptSettings>
    {
        private readonly IConsoleOutput console;
        private readonly IProgressReporter? progressReporter;
        private readonly ITypeScriptCodeGeneratorFactory factory;
        private readonly IGeneralOptions options;

        public TypeScriptCommand(
            IConsoleOutput console,
            IProgressReporter? progressReporter,
            ITypeScriptCodeGeneratorFactory factory,
            IGeneralOptions options)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.progressReporter = progressReporter ?? throw new ArgumentNullException(nameof(progressReporter));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, TypeScriptSettings settings)
        {
            AnsiConsole.MarkupLine("[bold green]🚀 Generating TypeScript code[/]");

            if (!settings.SkipLogging)
            {
                Logger.Instance.TrackFeatureUsage("TypeScript", "CLI");
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
                options);

            await Task.Run(() => generator.GenerateCode(progressReporter));

            console.WriteMarkup($"[green]✅ TypeScript code generated in:[/] {settings.OutputPath}");
            console.WriteLine("");
            console.WriteSignature();

            return ResultCodes.Success;
        }
    }
}