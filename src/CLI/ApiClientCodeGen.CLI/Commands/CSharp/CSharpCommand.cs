using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    [ExcludeFromCodeCoverage]
    public class CSharpCommand : AsyncCommand<BaseCommandSettings>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, BaseCommandSettings settings)
        {
            // This is a parent command, show help
            return 0;
        }
    }
}
