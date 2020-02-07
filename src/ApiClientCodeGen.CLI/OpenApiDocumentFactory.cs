using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using NSwag;

namespace ApiClientCodeGen.CLI
{
    [ExcludeFromCodeCoverage]
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