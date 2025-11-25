using System;
using Rapicgen.Core;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public class NSwagSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "NSwagCodeGenerator";
        
        private readonly INSwagCodeGeneratorFactory factory;
        private readonly INSwagOptions options;

        public NSwagSingleFileCustomTool()
        {
             var processLauncher = new ProcessLauncher();
             var dependencyInstaller = new DependencyInstaller(
                 new NpmInstaller(processLauncher),
                 new FileDownloader(new WebDownloader()),
                 processLauncher);
             
             this.factory = new NSwagCodeGeneratorFactory(processLauncher, dependencyInstaller);
             this.options = new DefaultNSwagOptions();
        }

        public NSwagSingleFileCustomTool(
            INSwagCodeGeneratorFactory factory,
            INSwagOptions options)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options);
    }
}
