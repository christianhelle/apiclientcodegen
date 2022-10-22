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
        public string? CustomAdditionalProperties { get; set; }
    }
}