using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ApiClientCodeGen.CLI.Options
{
    public class GeneralOptions : IGeneralOptions
    {
        public GeneralOptions()
        {
            JavaPath = PathProvider.GetJavaPath();
            NpmPath = PathProvider.GetNpmPath();
            NSwagPath = PathProvider.GetNSwagPath();
            SwaggerCodegenPath = PathProvider.GetSwaggerCodegenPath();
            OpenApiGeneratorPath = PathProvider.GetOpenApiGeneratorPath();
        }

        public string JavaPath { get; set; }
        public string NpmPath { get; set; }
        public string NSwagPath { get; set; }
        public string SwaggerCodegenPath { get; set; }
        public string OpenApiGeneratorPath { get; set; }
    }
}