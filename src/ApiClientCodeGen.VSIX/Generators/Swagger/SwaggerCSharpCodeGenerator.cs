using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger
{
    public class SwaggerCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string swaggerFile;
        private readonly string defaultNamespace;

        public SwaggerCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            VerifySwaggerCodegenCli();

            var output = Path.Combine(
                Path.GetDirectoryName(swaggerFile) ?? throw new InvalidOperationException(),
                "TempApiClient");

            Directory.CreateDirectory(output);

            var arguments =
                $"-jar swagger-codegen-cli.jar generate " +
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

        private static void VerifySwaggerCodegenCli()
        {
            const string swaggerCli = "swagger-codegen-cli.jar";
            if (File.Exists(swaggerCli)) 
                return;

            const string swaggerCliUrl =
                "http://central.maven.org/maven2/io/swagger/swagger-codegen-cli/2.3.1/swagger-codegen-cli-2.3.1.jar";

            new WebClient()
                .DownloadFile(
                    swaggerCliUrl,
                    swaggerCli);
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
