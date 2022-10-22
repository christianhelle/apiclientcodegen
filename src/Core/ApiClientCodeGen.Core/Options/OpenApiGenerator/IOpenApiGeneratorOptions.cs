﻿namespace Rapicgen.Core.Options.OpenApiGenerator
{
    public interface IOpenApiGeneratorOptions
    {
        bool EmitDefaultValue { get; set; }
        bool MethodArgument { get; set; }
        bool GeneratePropertyChanged { get; set; }
        bool UseCollection { get; set; }
        bool UseDateTimeOffset { get; set; }
        OpenApiSupportedTargetFramework TargetFramework { get; set; }
        string? CustomAdditionalProperties { get; set; }
    }
}