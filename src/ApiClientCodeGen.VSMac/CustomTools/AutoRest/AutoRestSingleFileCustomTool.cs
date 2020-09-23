using System;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
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
        private readonly IOpenApiDocumentFactory documentFactory;

        public AutoRestSingleFileCustomTool()
            : this(
                Container.Instance.Resolve<IAutoRestCodeGeneratorFactory>(),
                Container.Instance.Resolve<IAutoRestOptions>(),
                Container.Instance.Resolve<IProcessLauncher>(),
                Container.Instance.Resolve<IOpenApiDocumentFactory>())
        {
        }

        public AutoRestSingleFileCustomTool(
            IAutoRestCodeGeneratorFactory factory,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                processLauncher,
                documentFactory);
    }
}