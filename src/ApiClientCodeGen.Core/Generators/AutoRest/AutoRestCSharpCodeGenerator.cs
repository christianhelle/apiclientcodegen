using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest
{
    public class AutoRestCSharpCodeGenerator : CodeGenerator
    {
        private readonly IAutoRestOptions options;
        private readonly IOpenApiDocumentFactory documentFactory;
        private static readonly object SyncLock = new object();

        public AutoRestCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory)
            : base(swaggerFile, defaultNamespace, processLauncher)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        public override string GenerateCode(IProgressReporter pGenerateProgress)
        {
            lock (SyncLock)
                return base.GenerateCode(pGenerateProgress);
        }

        protected override string GetArguments(string outputFile)
        {
            var args = "--csharp " +
                       $"--input-file=\"{SwaggerFile}\" " +
                       $"--output-file=\"{outputFile}\" " +
                       $"--namespace=\"{DefaultNamespace}\" ";

            var document = documentFactory.GetDocument(SwaggerFile).GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(document.OpenApi) && 
                Version.TryParse(document.OpenApi, out var openApiVersion) && 
                openApiVersion > Version.Parse("3.0.0"))
            {
                args += "--v3 ";
            }

            if (options.AddCredentials)
                args += "--add-credentials ";

            args += $"--client-side-validation=\"{options.ClientSideValidation}\" ";
            args += $"--sync-methods=\"{options.SyncMethods}\" ";

            if (options.UseDateTimeOffset)
                args += "--use-datetimeoffset ";

            if (options.UseInternalConstructors)
                args += " --use-internal-constructors ";

            if (options.OverrideClientName)
            {
                var file = new FileInfo(SwaggerFile);
                var name = file.Name
                    .Replace(" ", string.Empty)
                    .Replace(file.Extension, string.Empty);

                args += $" --override-client-name=\"{name}\"";
            }

            return args;
        }

        protected override string GetCommand()
        {
            DependencyDownloader.InstallAutoRest();
            return PathProvider.GetAutoRestPath();
        }
    }
}