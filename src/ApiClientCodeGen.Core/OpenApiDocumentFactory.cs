using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using NSwag;

namespace ApiClientCodeGen.CLI
{
    [ExcludeFromCodeCoverage]
    public class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public Task<OpenApiDocument> GetDocument(string swaggerFile)
        {
            return swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                : OpenApiDocument.FromFileAsync(swaggerFile);
        }
    }
}