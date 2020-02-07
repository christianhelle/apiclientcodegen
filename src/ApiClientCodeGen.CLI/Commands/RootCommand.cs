using System;
using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command(Name = "run", ThrowOnUnexpectedArgument = false, OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NSwagCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiGeneratorCommand))]
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
