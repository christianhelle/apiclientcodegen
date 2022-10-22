using System.Diagnostics.CodeAnalysis;

namespace Rapicgen.Core.Options.OpenApiGenerator
{
    [ExcludeFromCodeCoverage]
    public class DefaultOpenApiGeneratorOptions : IOpenApiGeneratorOptions
    {
        public bool EmitDefaultValue { get; set; } = true;

        public bool MethodArgument { get; set; } = true;

        public bool GeneratePropertyChanged { get; set; } = false;

        public bool UseCollection { get; set; } = false;

        public bool UseDateTimeOffset { get; set; } = false;

        public OpenApiSupportedTargetFramework TargetFramework { get; set; }

        public string? CustomAdditionalProperties { get; set; }
    }
}