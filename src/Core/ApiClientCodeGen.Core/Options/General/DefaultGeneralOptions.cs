using Rapicgen.Core.External;

namespace Rapicgen.Core.Options.General
{
    public class DefaultGeneralOptions : IGeneralOptions
    {
        public string JavaPath => PathProvider.GetJavaPath();
        public string NpmPath => PathProvider.GetNpmPath();
        public string NSwagPath => PathProvider.GetNSwagPath();
        public string SwaggerCodegenPath => PathProvider.GetSwaggerCodegenPath();
        public string OpenApiGeneratorPath => PathProvider.GetOpenApiGeneratorPath();
        public bool? InstallMissingPackages => true;
    }
}