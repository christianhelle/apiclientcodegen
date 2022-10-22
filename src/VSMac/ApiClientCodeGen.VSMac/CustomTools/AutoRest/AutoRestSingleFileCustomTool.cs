using System;
using System.IO;
using System.Threading.Tasks;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.AutoRest;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.AutoRest
{
    public class AutoRestSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "AutoRestCodeGenerator";

        private readonly IAutoRestCodeGeneratorFactory factory;
        private readonly IAutoRestOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly IDependencyInstaller dependencyInstaller;

        public AutoRestSingleFileCustomTool()
            : this(
                Container.Instance.Resolve<IAutoRestCodeGeneratorFactory>(),
                Container.Instance.Resolve<IAutoRestOptions>(),
                Container.Instance.Resolve<IProcessLauncher>(),
                Container.Instance.Resolve<IOpenApiDocumentFactory>(),
                Container.Instance.Resolve<IDependencyInstaller>())
        {
        }

        public AutoRestSingleFileCustomTool(
            IAutoRestCodeGeneratorFactory factory,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.dependencyInstaller = dependencyInstaller;
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                processLauncher,
                documentFactory,
                dependencyInstaller);
    }
}