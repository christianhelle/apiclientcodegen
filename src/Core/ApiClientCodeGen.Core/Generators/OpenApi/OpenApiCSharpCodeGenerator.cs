using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi
{
    public class OpenApiCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly IOpenApiGeneratorOptions openApiGeneratorOptions;
        private readonly IProcessLauncher processLauncher;
        private readonly IDependencyInstaller dependencyInstaller;
        private readonly string swaggerFile;

        public OpenApiCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IOpenApiGeneratorOptions openApiGeneratorOptions,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.swaggerFile = swaggerFile;
            this.defaultNamespace = defaultNamespace;
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.openApiGeneratorOptions = openApiGeneratorOptions ??
                                           throw new ArgumentNullException(nameof(openApiGeneratorOptions));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller =
                dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
            javaPathProvider = new JavaPathProvider(options, processLauncher);
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);

                var jarFile = options.OpenApiGeneratorPath;
                if (!File.Exists(jarFile))
                {
                    Trace.WriteLine(jarFile + " does not exist");
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

                if (string.IsNullOrWhiteSpace(openApiGeneratorOptions.CustomAdditionalProperties))
                {
                    arguments +=
                        "--additional-properties " +
                        $"optionalEmitDefaultValues={openApiGeneratorOptions.EmitDefaultValue} " +
                        "--additional-properties " +
                        $"optionalMethodArguments={openApiGeneratorOptions.MethodArgument} " +
                        "--additional-properties " +
                        $"generatePropertyChanged={openApiGeneratorOptions.GeneratePropertyChanged} " +
                        "--additional-properties " +
                        $"useCollection={openApiGeneratorOptions.UseCollection} " +
                        "--additional-properties " +
                        $"useDateTimeOffset={openApiGeneratorOptions.UseDateTimeOffset} " +
                        "--additional-properties " +
                        $"targetFramework={openApiGeneratorOptions.TargetFramework.GetDescription()} ";
                }
                else
                {
                    arguments += openApiGeneratorOptions.CustomAdditionalProperties;
                }


                processLauncher.Start(
                    javaPathProvider.GetJavaExePath(),
                    arguments,
                    Path.GetDirectoryName(swaggerFile));

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