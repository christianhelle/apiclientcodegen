using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    public interface IOpenApiGeneratorFactory
    {
        ICodeGenerator Create(
            string generator,
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class OpenApiGeneratorFactory : IOpenApiGeneratorFactory
    {
        public ICodeGenerator Create(
            string generator,
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new OpenApiCodeGenerator(
                swaggerFile,
                outputPath,
                generator,
                options,
                processLauncher,
                dependencyInstaller);
    }
}