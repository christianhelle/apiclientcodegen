using System;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using MonoDevelop.Core;
using MonoDevelop.Ide.CustomTools;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public class NSwagSingleFileCustomTool : BaseSingleFileCustomTool
    {
        private readonly INSwagCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;

        public NSwagSingleFileCustomTool()
            : this(
                Container.Instance.Resolve<INSwagCodeGeneratorFactory>(),
                Container.Instance.Resolve<IOpenApiDocumentFactory>(),
                Container.Instance.Resolve<INSwagOptions>())
        {
        }

        public NSwagSingleFileCustomTool(
            INSwagCodeGeneratorFactory factory,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.openApiDocumentFactory = openApiDocumentFactory ??
                                          throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task OnGenerate(
            ProgressMonitor monitor,
            ProjectFile file,
            SingleFileCustomToolResult result)
        {
            var path = file.FilePath.ChangeExtension(".cs");
            result.GeneratedFilePath = path;

            var customToolNamespace = file.CustomToolNamespace;
            if (string.IsNullOrWhiteSpace(customToolNamespace))
                customToolNamespace = CustomToolService.GetFileNamespace(file, path);

            var generator = factory.Create(
                file.FilePath,
                customToolNamespace,
                options,
                openApiDocumentFactory);

            var progressReporter = new ProgressReporter(monitor);
            var contents = await Task.Run(() => generator.GenerateCode(progressReporter));
            await Task.Run(() => File.WriteAllText(path, contents));
        }
    }
}