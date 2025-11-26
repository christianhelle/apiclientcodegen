using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;

namespace Rapicgen.CLI.Commands.CSharp
{
    public interface INSwagCodeGeneratorFactory
    {
        ICodeGenerator Create(string swaggerFile,
            string defaultNamespace,
            INSwagOptions options);
    }

    public class NSwagCodeGeneratorFactory : INSwagCodeGeneratorFactory
    {
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public NSwagCodeGeneratorFactory(
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.processLauncher = processLauncher;
            this.dependencyInstaller = dependencyInstaller;
        }

        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            INSwagOptions options)
            => new NSwagCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                processLauncher,
                dependencyInstaller,
                options);
    }
}