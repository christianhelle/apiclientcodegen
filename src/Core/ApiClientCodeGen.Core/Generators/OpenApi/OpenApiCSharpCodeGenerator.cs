using System;
using System.IO;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.External;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.Generators.OpenApi
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
            string arguments = null!;
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
                    Path.GetTempPath(),
                    Guid.NewGuid().ToString("N"),
                    "TempApiClient");

                Directory.CreateDirectory(output);
                pGenerateProgress?.Progress(40);

                arguments = $"-jar \"{jarFile}\" generate " +
                            "--generator-name csharp " +
                            $"--input-spec \"{Path.GetFileName(swaggerFile)}\" " +
                            $"--output \"{output}\" " +
                            $"--package-name \"{defaultNamespace}\" " +
                            "--global-property apiTests=false " +
                            "--global-property modelTests=false " +
                            $"--global-property skipFormModel={openApiGeneratorOptions.SkipFormModel} " +
                            "--skip-overwrite ";

                if (openApiGeneratorOptions.UseConfigurationFile)
                {
                    var extension = Path.GetExtension(swaggerFile);
                    var configFilename = swaggerFile.Replace(extension, $".config{extension}");
                    var jsonConfigFilename = swaggerFile.Replace(extension, ".config.json");
                    var yamlConfigFilename = swaggerFile.Replace(extension, ".config.yaml");

                    var configFilenames = new[] { configFilename, jsonConfigFilename, yamlConfigFilename };
                    var configFile = Array.Find(configFilenames, File.Exists);
                    if (configFile != null)
                    {
                        arguments += $"-c \"{configFile}\" ";
                    }
                }

                if (!arguments.Contains("-c ") &&
                    string.IsNullOrWhiteSpace(openApiGeneratorOptions.CustomAdditionalProperties))
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

                if (!string.IsNullOrWhiteSpace(openApiGeneratorOptions.TemplatesPath))
                {
                    var templatesPath = openApiGeneratorOptions.TemplatesPath;
                    if (!Directory.Exists(templatesPath))
                    {
                        templatesPath = Path.Combine(Path.GetDirectoryName(swaggerFile)!, templatesPath);
                    }
                    arguments += $"-t \"{templatesPath}\" ";
                }

                var java = javaPathProvider.GetJavaExePath();
                using var context = new DependencyContext("OpenAPI Generator", $"{java} {arguments}");
                processLauncher.Start(java, arguments, Path.GetDirectoryName(swaggerFile));
                context.Succeeded();

                pGenerateProgress?.Progress(80);

                if (!openApiGeneratorOptions.GenerateMultipleFiles)
                {
                    var generatedCode = GeneratedCode.PrefixAutogeneratedCodeHeader(
                        Sanitize(CSharpFileMerger.MergeFilesAndDeleteSource(output)),
                        "OpenAPI Generator",
                        "v7.12.0");

                    // v7.12.0 generates a class that conflicts with System.Net.CookieContainer
                    generatedCode = generatedCode
                        .Replace("class CookieContainer", "class TokenCookieContainer")
                        .Replace("<CookieContainer>", "<TokenCookieContainer>");

                    return generatedCode;
                }
                else
                {
                    CSharpFileMerger.CopyFilesAndDeleteSource(
                         Path.Combine(output, "src", defaultNamespace),
                         Path.GetDirectoryName(swaggerFile)!);
                }

                return string.Empty;
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }

        private static string Sanitize(string code) =>
            code.Replace("using System.Net.Mime;", null);
    }
}
