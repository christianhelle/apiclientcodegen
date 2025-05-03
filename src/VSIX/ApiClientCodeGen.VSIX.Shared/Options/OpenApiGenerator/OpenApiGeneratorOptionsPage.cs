using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Options.OpenApiGenerator;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace Rapicgen.Options.OpenApiGenerator
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public class OpenApiGeneratorOptionsPage : DialogPage, IOpenApiGeneratorOptions
    {
        public const string Name = "OpenAPI Generator";

        [Category(Name)]
        [DisplayName("Emit Default Value")]
        [Description("Set to True if the default value for a member should be generated in the serialization stream. " +
                     "Setting this to False is not a recommended practice. " +
                     "It should only be done if there is a specific need to do so, " +
                     "such as for interoperability or to reduce data size.")]
        public bool EmitDefaultValue { get; set; } = true;

        [Category(Name)]
        [DisplayName("Optional Method Arguments")]
        [Description("C# Optional method argument, e.g. void square(int x=10) (.net 4.0+ only).")]
        public bool MethodArgument { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Property Changed")]
        public bool GeneratePropertyChanged { get; set; } = false;

        [Category(Name)]
        [DisplayName("Use ICollection<T>")]
        [Description("Deserialize array types to Collection<T> instead of List<T>.")]
        public bool UseCollection { get; set; } = false;

        [Category(Name)]
        [DisplayName("Use DateTimeOffset")]
        [Description("Use DateTimeOffset to model date-time properties")]
        public bool UseDateTimeOffset { get; set; } = false;

        [Category(Name)]
        [DisplayName("Target Framework")]
        [Description("The target .NET framework version")]
        public OpenApiSupportedTargetFramework TargetFramework { get; set; } = OpenApiSupportedTargetFramework.NetStandard21;

        [Category(Name)]
        [DisplayName("Custom Addition Properties")]
        [Description("Setting this will override all the other additional properties")]
        public string? CustomAdditionalProperties { get; set; } = null!;

        [Category(Name)]
        [DisplayName("Skip Form Model")]
        [Description("To skip models defined as the form parameters in 'requestBody'")]
        public bool SkipFormModel { get; set; } = true;

        [Category(Name)]
        [DisplayName("Templates Path")]
        [Description("Path to the folder containing the custom Mustache templates. " +
                     "This should be either an absolute path or a path relative to the swagger file.")]
        public string? TemplatesPath { get; set; } = null!;

        [Category(Name)]
        [DisplayName("Use Configuration File")]
        [Description("Use the configuration file if present.")]
        public bool UseConfigurationFile { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Multiple Files")]
        [Description("Generate multiple files for each operation. This only works for SDK style projects")]
        public bool GenerateMultipleFiles { get; set; }

        [Category(Name)]
        [DisplayName("Version")]
        [Description("The version of the generator to use.")]
        public OpenApiSupportedVersion Version { get; set; }

        [Category(Name)]
        [DisplayName("Custom HTTP User-Agent")]
        [Description("Sets the User-Agent header value to be sent in the HTTP request.")]
        public string? HttpUserAgent { get; set; }
    }
}