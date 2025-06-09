using System.ComponentModel;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    public class RefitterSettings : CodeGeneratorSettings
    {
        [CommandOption("--skip-generate-contracts")]
        [Description("Set this to skip generating the contract types (default: Enabled)")]
        public bool SkipGenerateContracts { get; set; }

        [CommandOption("--skip-generate-xml-doc-code-comments")]
        [Description("Set this to skip generating XML doc style code comments (default: Enabled)")]
        public bool SkipGenerateXmlDocCodeComments { get; set; }

        [CommandOption("--return-api-response")]
        [Description("Set this to wrap the returned the contract types in IApiResponse<T> (default: Disabled)")]
        public bool ReturnIApiResponse { get; set; }

        [CommandOption("--generate-internal-types")]
        [Description("Set this to generate the API interface and contract types using the internal accessbility modifier (default modifier: public)")]
        public bool GenerateInternalTypes { get; set; }

        [CommandOption("--cancellation-tokens")]
        [Description("Set this to generate the API interface with Cancellation Tokens")]
        public bool CancellationTokens { get; set; }

        [CommandOption("--no-operation-headers")]
        [Description("Don't generate operation headers")]
        public bool NoOperationHeaders { get; set; }

        [CommandOption("--multiple-files")]
        [Description("Generate multiple files")]
        public bool MultipleFiles { get; set; }

        [CommandOption("--settings-file <SETTINGS_FILE>")]
        [Description("Path to a .refitter settings file to use for code generation")]
        public string? SettingsFile { get; set; }
    }
}