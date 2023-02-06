using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;

namespace Rapicgen.CLI.Commands.CSharp
{
    [ExcludeFromCodeCoverage]
    [Command("csharp", Description = "Generate C# API clients")]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(KiotaCommand),
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
