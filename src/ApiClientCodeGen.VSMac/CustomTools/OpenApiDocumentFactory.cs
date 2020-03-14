using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools
{
    public class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public OpenApiDocument GetDocument(string swaggerFile)
        {
            return OpenApiDocument.FromFileAsync(swaggerFile)
                .GetAwaiter()
                .GetResult();
        }
    }
}