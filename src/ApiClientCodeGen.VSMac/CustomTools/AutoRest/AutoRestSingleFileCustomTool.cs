using System;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
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

        public AutoRestSingleFileCustomTool()
            : this(
                Container.Instance.Resolve<IAutoRestCodeGeneratorFactory>(),
                Container.Instance.Resolve<IAutoRestOptions>(),
                Container.Instance.Resolve<IProcessLauncher>())
        {
        }

        public AutoRestSingleFileCustomTool(
            IAutoRestCodeGeneratorFactory factory,
            IAutoRestOptions options,
            IProcessLauncher processLauncher)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                processLauncher);
    }
}