using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command("csharp", Description = "Generate C# API clients")]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NSwagCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiGeneratorCommand))]
    public class CSharpCommand
    {
        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(false);
            return 0;
        }
    }
}
