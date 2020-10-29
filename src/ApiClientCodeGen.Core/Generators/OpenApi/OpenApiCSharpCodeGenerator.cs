using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi
{
    public class OpenApiCSharpCodeGenerator : CodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly string swaggerFile;

        public OpenApiCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher)
            : base(swaggerFile, defaultNamespace, processLauncher)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            javaPathProvider = new JavaPathProvider(options, processLauncher);
        }

        public override string GenerateCode(IProgressReporter pGenerateProgress)
        {
            base.GenerateCode(pGenerateProgress);
            return CSharpFileMerger.MergeFilesAndDeleteSource(GetOutputPath());
        }

        protected override string GetArguments(string outputFile)
        {
            var output = GetOutputPath();

            return "generate " +
                   "-g csharp " +
                   $"--input-spec \"{swaggerFile}\" " +
                   $"--output \"{output}\" " +
                   "-DapiTests=false -DmodelTests=false " +
                   $"-DpackageName={defaultNamespace} " +
                   "--skip-overwrite ";
        }

        private string GetOutputPath()
        {
            var output = Path.Combine(
                Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                "TempApiClient");

            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            return output;
        }

        protected override string GetCommand()
        {
            DependencyDownloader.InstallOpenApiGenerator();
            return PathProvider.GetOpenApiGeneratorPath();
        }
    }
}