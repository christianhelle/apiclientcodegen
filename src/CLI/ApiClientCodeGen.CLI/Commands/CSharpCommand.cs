using System.Diagnostics.CodeAnalysis;
using McMaster.Extensions.CommandLineUtils;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command("csharp", Description = "Generate C# API client code")]
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