using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands
{
    public class CodeGeneratorSettings : BaseCommandSettings
    {
        [CommandArgument(0, "<SWAGGER_FILE>")]
        [Description("Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; } = null!;

        [CommandArgument(1, "[NAMESPACE]")]
        [Description("Default namespace to in the generated code")]
        public string DefaultNamespace { get; set; } = "GeneratedCode";

        [CommandArgument(2, "[OUTPUT_FILE]")]
        [Description("Output filename to write the generated code to")]
        public string? OutputFile { get; set; }
    }
}