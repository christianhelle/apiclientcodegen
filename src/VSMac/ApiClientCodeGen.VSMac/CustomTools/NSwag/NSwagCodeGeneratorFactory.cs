using System;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public class NSwagCodeGeneratorFactory : INSwagCodeGeneratorFactory
    {
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;

        public NSwagCodeGeneratorFactory(
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            INSwagOptions options)
            => new NSwagCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}
