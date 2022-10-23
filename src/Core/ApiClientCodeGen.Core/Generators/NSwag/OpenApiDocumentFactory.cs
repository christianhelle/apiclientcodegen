using System.Threading.Tasks;
using NSwag;

namespace Rapicgen.Core.Generators.NSwag
{
    public interface IOpenApiDocumentFactory
    {
        Task<OpenApiDocument> GetDocumentAsync(string swaggerFile);
    }
}