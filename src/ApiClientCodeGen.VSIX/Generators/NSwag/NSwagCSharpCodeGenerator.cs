using System;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public class NSwagCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string swaggerFile;
        private readonly string defaultNamespace;

        public NSwagCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            var document = SwaggerDocument
                .FromFileAsync(swaggerFile)
                .GetAwaiter()
                .GetResult();
            
            var settings = new SwaggerToCSharpClientGeneratorSettings
            {
                ClassName = "ApiClient",
                InjectHttpClient = true,
                GenerateClientInterfaces = true,
                GenerateDtoTypes = true,
                CSharpGeneratorSettings =
                {
                    Namespace = defaultNamespace,
                    ClassStyle = CSharpClassStyle.Inpc
                }
            };

            var generator = new SwaggerToCSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            return code;
        }
    }
}
