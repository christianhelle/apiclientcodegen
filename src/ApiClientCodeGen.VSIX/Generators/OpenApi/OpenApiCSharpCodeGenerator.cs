using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi
{
    public class OpenApiCSharpCodeGenerator : ICodeGenerator
    {
        private const string DownloadUrl = "http://central.maven.org/maven2/org/openapitools/openapi-generator-cli/4.0.0/openapi-generator-cli-4.0.0.jar";
        private const string Checksum = "61574C43BEC9B6EDD54E2DD0993F81D5";
        private readonly string swaggerFile;
        private readonly string defaultNamespace;
        private readonly string cliPath;

        public OpenApiCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            cliPath = Path.Combine(Path.GetTempPath(), "openapi-generator-cli.jar");
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                pGenerateProgress.Progress(10);

                VerifySwaggerCodegenCli();
                pGenerateProgress.Progress(30);

                var output = Path.Combine(
                    Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                    "TempApiClient");

                Directory.CreateDirectory(output);
                pGenerateProgress.Progress(40);

                var arguments =
                    $"-jar \"{cliPath}\" generate " +
                    "-g csharp " +
                    $"--input-spec \"{swaggerFile}\" " +
                    $"--output \"{output}\" " +
                    "-DapiTests=false -DmodelTests=false " +
                    $"-DpackageName={defaultNamespace} " +
                    "--skip-overwrite ";

                ProcessHelper.StartProcess("java", arguments);
                pGenerateProgress.Progress(80);

                return MergeFilesAndDeleteFolder(output);
            }
            finally
            {
                pGenerateProgress.Progress(90);
            }
        }

        private void VerifySwaggerCodegenCli()
        {
            if (File.Exists(cliPath) && FileHelper.CalculateMd5(cliPath) == Checksum)
                return;

            new WebClient().DownloadFile(DownloadUrl, cliPath);
        }

        private static string MergeFilesAndDeleteFolder(string output)
        {
            try
            {
                return CSharpFileMerger.MergeFiles(output);
            }
            finally
            {
                try
                {
                    Directory.Delete(output, true);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
        }
    }
}
