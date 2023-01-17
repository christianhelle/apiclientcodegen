using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.OpenApi
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

        protected override string GetGeneratorArguments() => "--skip-validate-spec";
    }
}