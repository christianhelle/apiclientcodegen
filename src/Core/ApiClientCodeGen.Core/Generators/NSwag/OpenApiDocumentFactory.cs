using System.Threading.Tasks;
using Rapicgen.Core.Models;

namespace Rapicgen.Core.Generators.NSwag
{
    public interface IOpenApiDocumentFactory
    {
        Task<SimpleOpenApiDocument> GetDocumentAsync(string swaggerFile);
    }
}