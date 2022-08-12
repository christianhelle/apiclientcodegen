using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NSwagCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiGeneratorCommand),
        typeof(JMeterCommand),
        typeof(TypeScriptCommand))]
    public class RootCommand
    {
        [Option(VerboseOption.Template, CommandOptionType.NoValue, Description = VerboseOption.Description)]
        public bool Verbose { get; set; }

        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(false);
            return 0;
        }
    }
}
