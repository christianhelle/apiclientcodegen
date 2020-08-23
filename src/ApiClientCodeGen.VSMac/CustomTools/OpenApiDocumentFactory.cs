using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public OpenApiDocument GetDocument(string swaggerFile)
        {
            return swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                    .GetAwaiter()
                    .GetResult()
                : OpenApiDocument.FromFileAsync(swaggerFile)
                    .GetAwaiter()
                    .GetResult();
        }
    }
}