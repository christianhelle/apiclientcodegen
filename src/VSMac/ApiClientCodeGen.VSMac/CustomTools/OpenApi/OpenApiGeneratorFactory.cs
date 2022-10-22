using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace ApiClientCodeGen.VSMac.CustomTools.OpenApi
{
    public interface IOpenApiGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions generalOptions,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class OpenApiGeneratorFactory : IOpenApiGeneratorFactory
    {
        public ICodeGenerator Create(string swaggerFile,
            string defaultNamespace,
            IGeneralOptions generalOptions,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new OpenApiCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                generalOptions,
                openApiGeneratorOptions,
                processLauncher,
                dependencyInstaller);
    }
}