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

        public string JavaPath { get; }
        public string NpmPath { get; }
        public string NSwagPath { get; }
        public string SwaggerCodegenPath { get; }
        public string OpenApiGeneratorPath { get; }
        public bool? InstallMissingPackages { get; } = true;
    }
}