using System;
using System.Diagnostics;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.OpenApiGenerator
{
    public class OpenApiGeneratorOptions : OptionsBase<IOpenApiGeneratorOptions, OpenApiGeneratorOptionsPage>, IOpenApiGeneratorOptions
    {
        public OpenApiGeneratorOptions(IOpenApiGeneratorOptions options)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

                EmitDefaultValue = options.EmitDefaultValue;
                MethodArgument = options.MethodArgument;
                GeneratePropertyChanged = options.GeneratePropertyChanged;
                UseCollection = options.UseCollection;
                UseDateTimeOffset = options.UseDateTimeOffset;
                TargetFramework = options.TargetFramework;
                CustomAdditionalProperties = options.CustomAdditionalProperties;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine($"EmitDefaultValue = {EmitDefaultValue}");

                EmitDefaultValue = true;
            }
        }

        public bool EmitDefaultValue { get; set; }
        public bool MethodArgument { get; set; }
        public bool GeneratePropertyChanged { get; set; }
        public bool UseCollection { get; set; }
        public bool UseDateTimeOffset { get; set; }
        public OpenApiSupportedTargetFramework TargetFramework { get; set; }
        public string? CustomAdditionalProperties { get; set; }
    }
}