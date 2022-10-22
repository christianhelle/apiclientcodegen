using System;
using System.Threading.Tasks;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.Swagger
{
    public class SwaggerSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "SwaggerCodeGenerator";
        
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly ISwaggerCodegenFactory factory;
        private readonly IDependencyInstaller dependencyInstaller;

        public SwaggerSingleFileCustomTool()
        :this(
            Container.Instance.Resolve<IGeneralOptions>(),
            Container.Instance.Resolve<IProcessLauncher>(),
            Container.Instance.Resolve<ISwaggerCodegenFactory>(),
            Container.Instance.Resolve<IDependencyInstaller>())
        {
        }

        public SwaggerSingleFileCustomTool(
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            ISwaggerCodegenFactory factory,
            IDependencyInstaller dependencyInstaller)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile, 
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                processLauncher,
                dependencyInstaller);
    }
}