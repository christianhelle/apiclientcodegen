using System;
using System.IO;

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

        private static string MergeFilesAndDeleteFolder(string output)
        {
            try
            {
                return CSharpFileMerger.MergeFiles(output);
            }
            finally
            {
                Directory.Delete(output, true);
            }
        }
    }
}
