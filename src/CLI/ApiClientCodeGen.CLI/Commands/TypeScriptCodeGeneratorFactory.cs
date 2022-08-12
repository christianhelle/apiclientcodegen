using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands
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