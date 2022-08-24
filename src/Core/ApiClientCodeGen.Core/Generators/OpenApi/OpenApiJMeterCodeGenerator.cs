using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi
{
    public class OpenApiJMeterCodeGenerator : OpenApiCodeGenerator
    {
        public OpenApiJMeterCodeGenerator(
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            : base(swaggerFile, outputPath, "jmeter", options, processLauncher, dependencyInstaller)
        {
        }
    }
}