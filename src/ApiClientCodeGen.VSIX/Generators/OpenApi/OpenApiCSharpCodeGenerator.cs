using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi
{
    public class OpenApiCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string swaggerFile;
        private readonly string defaultNamespace;
        private readonly string cliPath;

        public OpenApiCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            cliPath = Path.Combine(Path.GetTempPath(), "openapi-generator-cli.jar");
        }

        public string GenerateCode()
        {
            VerifySwaggerCodegenCli();

            var output = Path.Combine(
                Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                "TempApiClient");

            Directory.CreateDirectory(output);

            var arguments =
                $"-jar \"{cliPath}\" generate " +
                $"-l csharp " +
                $"--input-spec \"{swaggerFile}\" " +
                $"--output \"{output}\" " +
                $"--api-package={defaultNamespace} " +
                $"--model-package={defaultNamespace} " +
                $"-DapiTests=false -DmodelTests=false --skip-overwrite " +
                $"-DpackageName={defaultNamespace}";

            ProcessHelper.StartProcess("java", arguments);
            return MergeFilesAndDeleteFolder(output);
        }

        private void VerifySwaggerCodegenCli()
        {
            if (File.Exists(cliPath)) 
                return;

            const string url =
                "http://central.maven.org/maven2/org/openapitools/openapi-generator-cli/3.3.4/openapi-generator-cli-3.3.4.jar";

            new WebClient()
                .DownloadFile(
                    url,
                    cliPath);
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
