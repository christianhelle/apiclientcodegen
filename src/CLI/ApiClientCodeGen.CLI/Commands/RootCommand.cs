using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    public class RootCommand : Command<RootCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandOption("-v|--verbose")]
            [Description("Show verbose output")]
            public bool Verbose { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            // Show help when no subcommand is specified
            return 0;
        }
    }
}
