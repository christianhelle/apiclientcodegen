using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General
{
    public class CustomPathOptions 
        : OptionsBase<IGeneralOptions, GeneralOptionPage>, IGeneralOptions
    {
        public CustomPathOptions(IGeneralOptions options = null)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

                JavaPath = options.JavaPath;
                NpmPath = options.NpmPath;
                NSwagPath = options.NSwagPath;
                SwaggerCodegenPath = options.SwaggerCodegenPath;
                OpenApiGeneratorPath = options.OpenApiGeneratorPath;
                InstallMissingPackages = options.InstallMissingPackages;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                
                JavaPath = PathProvider.GetJavaPath();
                NpmPath = PathProvider.GetNpmPath();
                NSwagPath = PathProvider.GetNSwagStudioPath();
                SwaggerCodegenPath = PathProvider.GetSwaggerCodegenPath();
                OpenApiGeneratorPath = PathProvider.GetOpenApiGeneratorPath();
                InstallMissingPackages = true;

                TraceLogger.WriteLine(Environment.NewLine);
                TraceLogger.WriteLine("Error reading user options. Reverting to default values");
                TraceLogger.WriteLine($"JavaPath = {JavaPath}");
                TraceLogger.WriteLine($"NpmPath = {NpmPath}");
                TraceLogger.WriteLine($"NSwagPath = {NSwagPath}");
                TraceLogger.WriteLine($"SwaggerCodegenPath = {SwaggerCodegenPath}");
                TraceLogger.WriteLine($"OpenApiGeneratorPath = {OpenApiGeneratorPath}");
                TraceLogger.WriteLine($"InstallMissingPackages = {InstallMissingPackages}");
            }
        }

        public string JavaPath { get; set; }
        public string NpmPath { get; set; }
        public string NSwagPath { get; set; }
        public string SwaggerCodegenPath { get; set; }
        public string OpenApiGeneratorPath { get; set; }
        public bool? InstallMissingPackages { get; }
    }
}
