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
            var path = Path.GetDirectoryName(swaggerFile);
            var output = Path.Combine(path, "TempApiClient");

            var arguments =
                "-jar swagger-codegen-cli.jar generate " +
                $"-i {swaggerFile} " +
                "-l csharp " +
                "-o ApiClient " +
                "-DapiTests=false " +
                "-DmodelTests=false";

            ProcessHelper.StartProcess("java", arguments);
            return FileHelper.ReadThenDelete(output);

            throw new NotImplementedException();
        }
    }
}
