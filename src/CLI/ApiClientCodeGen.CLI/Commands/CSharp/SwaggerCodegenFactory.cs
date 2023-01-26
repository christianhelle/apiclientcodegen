using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.CLI.Commands.CSharp
{
    public interface ISwaggerCodegenFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller);
    }

    public class SwaggerCodegenFactory : ISwaggerCodegenFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            => new SwaggerCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}