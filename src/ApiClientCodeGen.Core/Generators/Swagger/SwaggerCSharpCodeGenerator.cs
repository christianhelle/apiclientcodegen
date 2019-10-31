using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger
{
    public class SwaggerCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly JavaPathProvider javaPathProvider;
        private readonly IGeneralOptions options;
        private readonly string swaggerFile;

        public SwaggerCSharpCodeGenerator(string swaggerFile, string defaultNamespace, IGeneralOptions options)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            javaPathProvider = new JavaPathProvider(options);
        }

        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);

                var jarFile = options.SwaggerCodegenPath;
                if (!File.Exists(jarFile))
                {
                    Trace.WriteLine(jarFile + " does not exist");
                    jarFile = DependencyDownloader.InstallSwaggerCodegenCli();
                }

                pGenerateProgress.Progress(30);

                var output = Path.Combine(
                    Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                    "TempApiClient");

                Directory.CreateDirectory(output);
                pGenerateProgress.Progress(40);

                var arguments =
                    $"-jar \"{jarFile}\" generate " +
                    "-l csharp " +
                    $"--input-spec \"{swaggerFile}\" " +
                    $"--output \"{output}\" " +
                    "-DapiTests=false -DmodelTests=false " +
                    $"-DpackageName={defaultNamespace} ";

                ProcessHelper.StartProcess(javaPathProvider.GetJavaExePath(), arguments);
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