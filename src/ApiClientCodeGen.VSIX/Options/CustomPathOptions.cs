using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class CustomPathOptions : IGeneralOptions
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
            }
            catch (Exception e)
            {
                JavaPath = PathProvider.GetJavaPath();
                NpmPath = PathProvider.GetNpmPath();
                NSwagPath = PathProvider.GetNSwagPath();
                SwaggerCodegenPath = PathProvider.GetSwaggerCodegenPath();
                OpenApiGeneratorPath = PathProvider.GetOpenApiGeneratorPath();

                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine($"JavaPath = {JavaPath}");
                Trace.WriteLine($"NpmPath = {NpmPath}");
                Trace.WriteLine($"NSwagPath = {NSwagPath}");
                Trace.WriteLine($"SwaggerCodegenPath = {SwaggerCodegenPath}");
                Trace.WriteLine($"OpenApiGeneratorPath = {OpenApiGeneratorPath}");
            }
        }

        [ExcludeFromCodeCoverage]
        private static IGeneralOptions GetFromDialogPage()
            => (IGeneralOptions)VsPackage.Instance.GetDialogPage(typeof(GeneralOptionPage));

        public string JavaPath { get; set; }
        public string NpmPath { get; set; }
        public string NSwagPath { get; set; }
        public string SwaggerCodegenPath { get; set; }
        public string OpenApiGeneratorPath { get; set; }
    }
}
