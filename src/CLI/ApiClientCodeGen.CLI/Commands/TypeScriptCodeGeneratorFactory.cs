using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands
{
    public interface ITypeScriptCodeGeneratorFactory
    {
        ICodeGenerator Create(
            OpenApiTypeScriptGenerator generator,
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class TypeScriptCodeGeneratorFactory : ITypeScriptCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            OpenApiTypeScriptGenerator generator,
            string swaggerFile,
            string outputPath,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new OpenApiTypeScriptCodeGenerator(
                swaggerFile,
                outputPath,
                generator,
                options,
                processLauncher,
                dependencyInstaller);
    }
}