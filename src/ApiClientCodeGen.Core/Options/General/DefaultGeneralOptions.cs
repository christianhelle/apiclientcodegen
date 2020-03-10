namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General
{
    public class DefaultGeneralOptions : IGeneralOptions
    {
        public DefaultGeneralOptions()
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