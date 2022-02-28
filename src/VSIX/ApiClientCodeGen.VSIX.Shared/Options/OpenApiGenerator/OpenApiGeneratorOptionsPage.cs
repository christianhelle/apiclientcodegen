using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.OpenApiGenerator
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
    }
}