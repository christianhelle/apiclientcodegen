using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command("csharp", Description = "Generate C# API client")]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NSwagCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiCSharpGeneratorCommand))]
    public class CSharpCommand
    {
        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(false);
            return 0;
        }
    }
}
