using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi
{
    public class OpenApiCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly string swaggerFile;

        public OpenApiCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            javaPathProvider = new JavaPathProvider(options, processLauncher);
        }

        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);

                var jarFile = options.OpenApiGeneratorPath;
                if (!File.Exists(jarFile))
                {
                    TraceLogger.WriteLine(jarFile + " does not exist");
                    jarFile = dependencyInstaller.InstallOpenApiGenerator();
                }

                pGenerateProgress.Progress(30);

                var output = Path.Combine(
                    Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                    Guid.NewGuid().ToString("N"),
                    "TempApiClient");

                Directory.CreateDirectory(output);
                pGenerateProgress.Progress(40);

                var arguments =
                    $"-jar \"{jarFile}\" generate " +
                    "--generator-name csharp-netcore " +
                    $"--input-spec \"{Path.GetFileName(swaggerFile)}\" " +
                    $"--output \"{output}\" " +
                    $"--package-name \"{defaultNamespace}\" " +
                    "--global-property apiTests=false,modelTests=false " +
                    "--skip-overwrite ";

                processLauncher.Start(javaPathProvider.GetJavaExePath(), arguments, Path.GetDirectoryName(swaggerFile));
                pGenerateProgress.Progress(80);

                return CSharpFileMerger.MergeFilesAndDeleteSource(output);
            }
            finally
            {
                pGenerateProgress.Progress(90);
            }
        }
    }
}