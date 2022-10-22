using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands
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