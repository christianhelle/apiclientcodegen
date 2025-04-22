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

        public bool SkipFormModel { get; set; }

        public string? TemplatesPath { get; set; }

        public bool UseConfigurationFile { get; set; } = false;

        public bool GenerateMultipleFiles { get; set; }

        public OpenApiSupportedVersion Version { get; set; }
    }
}