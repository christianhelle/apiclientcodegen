using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi
{
    public class OpenApiTypeScriptCodeGenerator : ICodeGenerator
    {
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly string swaggerFile;
        private readonly string outputPath;
        private readonly OpenApiTypeScriptGenerator typeScriptGenerator;

        public OpenApiTypeScriptCodeGenerator(
            string swaggerFile,
            string outputPath,
            OpenApiTypeScriptGenerator typeScriptGenerator,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.swaggerFile = swaggerFile;
            this.outputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
            this.typeScriptGenerator = typeScriptGenerator;
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            javaPathProvider = new JavaPathProvider(options, processLauncher);
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            try
            {
                pGenerateProgress?.Progress(10);

                var jarFile = options.OpenApiGeneratorPath;
                if (!File.Exists(jarFile))
                {
                    Trace.WriteLine(jarFile + " does not exist");
                    jarFile = dependencyInstaller.InstallOpenApiGenerator();
                }

                pGenerateProgress?.Progress(30);

                var output = Path.Combine(
                    Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                    outputPath);

                Directory.CreateDirectory(output);
                pGenerateProgress?.Progress(40);

                var arguments =
                    $"-jar \"{jarFile}\" generate " +
                    $"--generator-name {GetGeneratorName(typeScriptGenerator)} " +
                    $"--input-spec \"{Path.GetFileName(swaggerFile)}\" " +
                    $"--output \"{output}\" ";


                processLauncher.Start(
                    javaPathProvider.GetJavaExePath(),
                    arguments,
                    Path.GetDirectoryName(swaggerFile));

                pGenerateProgress?.Progress(80);
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }

            return string.Empty;
        }

        private static string GetGeneratorName(OpenApiTypeScriptGenerator openApiTypeScriptGenerator)
            => openApiTypeScriptGenerator switch
            {
                OpenApiTypeScriptGenerator.ReduxQuery => "typescript-redux-query",
                _ => "typescript-" + openApiTypeScriptGenerator.ToString().ToLower()
            };
    }
}