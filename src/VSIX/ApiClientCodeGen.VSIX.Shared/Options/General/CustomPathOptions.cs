using System;
using System.Diagnostics;
using Rapicgen.Core.External;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Options.General
{
    public class CustomPathOptions
        : OptionsBase<IGeneralOptions, GeneralOptionPage>, IGeneralOptions
    {
        public CustomPathOptions(IGeneralOptions? options = null)
        {
            try
            {
                options ??= GetFromDialogPage();
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

                
                Logger.Instance.WriteLine(Environment.NewLine);
                Logger.Instance.WriteLine("Error reading user options. Reverting to default values");
                Logger.Instance.WriteLine($"JavaPath = {JavaPath}");
                Logger.Instance.WriteLine($"NpmPath = {NpmPath}");
                Logger.Instance.WriteLine($"NSwagPath = {NSwagPath}");
                Logger.Instance.WriteLine($"SwaggerCodegenPath = {SwaggerCodegenPath}");
                Logger.Instance.WriteLine($"OpenApiGeneratorPath = {OpenApiGeneratorPath}");
                Logger.Instance.WriteLine($"InstallMissingPackages = {InstallMissingPackages}");
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
