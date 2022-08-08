using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
{
    public interface IJMeterCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class JMeterCodeGeneratorFactory : IJMeterCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new OpenApiJMeterCodeGenerator(
                swaggerFile,
                outputPath,
                options,
                processLauncher,
                dependencyInstaller);
    }
}