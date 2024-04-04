using System;
using System.Diagnostics;
using System.IO;
using Rapicgen.Core.External;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.OpenApi
{
    public class OpenApiCodeGenerator : ICodeGenerator
    {
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly string swaggerFile;
        private readonly string outputPath;
        private readonly string generator;

        public OpenApiCodeGenerator(
            string swaggerFile,
            string outputPath,
            string generator,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.swaggerFile = swaggerFile;
            this.outputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
            this.generator = generator;
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
                    Logger.Instance.WriteLine(jarFile + " does not exist");
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
                    $"--generator-name {GetGeneratorName()} " +
                    $"--input-spec \"{Path.GetFileName(swaggerFile)}\" " +
                    $"--output \"{output}\" ";
                
                arguments += GetGeneratorArguments();

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

        protected virtual string GetGeneratorArguments() => string.Empty;

        protected virtual string GetGeneratorName() => generator;
    }
}