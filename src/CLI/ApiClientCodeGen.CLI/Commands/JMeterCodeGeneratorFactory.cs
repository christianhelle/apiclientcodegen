using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands
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