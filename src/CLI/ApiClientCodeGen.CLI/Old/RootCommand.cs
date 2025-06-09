using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using McMaster.Extensions.CommandLineUtils;
using Rapicgen.CLI.Commands.CSharp;

namespace Rapicgen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command]
    [Subcommand(
        typeof(CSharpCommand),
        typeof(JMeterCommand),
        typeof(TypeScriptCommand),
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
