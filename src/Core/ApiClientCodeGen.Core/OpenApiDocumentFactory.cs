using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Rapicgen.Core.Generators.NSwag;
using NSwag;

namespace Rapicgen.Core
{
    [ExcludeFromCodeCoverage]
    public class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public Task<OpenApiDocument> GetDocumentAsync(string swaggerFile)
        {
            return swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                : OpenApiDocument.FromFileAsync(swaggerFile);
        }
    }
}