using System;
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
                UseConfigurationFile = options.UseConfigurationFile;
                GenerateMultipleFiles = options.GenerateMultipleFiles;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine($"EmitDefaultValue = {EmitDefaultValue}");
                Logger.Instance.WriteLine($"MethodArgument = {MethodArgument}");
                Logger.Instance.WriteLine($"GeneratePropertyChanged = {GeneratePropertyChanged}");
                Logger.Instance.WriteLine($"UseCollection = {UseCollection}");
                Logger.Instance.WriteLine($"UseDateTimeOffset = {UseDateTimeOffset}");
                Logger.Instance.WriteLine($"TargetFramework = {TargetFramework}");
                Logger.Instance.WriteLine($"CustomAdditionalProperties = {CustomAdditionalProperties}");
                Logger.Instance.WriteLine($"SkipFormModel = {SkipFormModel}");
                Logger.Instance.WriteLine($"TemplatesPath = {TemplatesPath}");
                Logger.Instance.WriteLine($"UseConfigurationFile = {UseConfigurationFile}");
                Logger.Instance.WriteLine($"GenerateMultipleFiles = {GenerateMultipleFiles}");

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
        public bool UseConfigurationFile { get; set; }
        public bool GenerateMultipleFiles { get; set; }
    }
}