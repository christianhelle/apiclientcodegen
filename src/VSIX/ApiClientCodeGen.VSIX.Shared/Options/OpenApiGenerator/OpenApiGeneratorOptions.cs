using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Options.OpenApiGenerator
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
                SkipFormModel = options.SkipFormModel;
                TemplatesPath = options.TemplatesPath;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine($"EmitDefaultValue = {EmitDefaultValue}");
                Trace.WriteLine($"MethodArgument = {MethodArgument}");
                Trace.WriteLine($"GeneratePropertyChanged = {GeneratePropertyChanged}");
                Trace.WriteLine($"UseCollection = {UseCollection}");
                Trace.WriteLine($"UseDateTimeOffset = {UseDateTimeOffset}");
                Trace.WriteLine($"TargetFramework = {TargetFramework}");
                Trace.WriteLine($"CustomAdditionalProperties = {CustomAdditionalProperties}");
                Trace.WriteLine($"SkipFormModel = {SkipFormModel}");
                Trace.WriteLine($"TemplatesPath = {TemplatesPath}");

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
        public bool SkipFormModel { get; set; }
        public string? TemplatesPath { get; set; }
    }
}