using System.Threading.Tasks;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag
{
    public interface IOpenApiDocumentFactory
    {
        Task<OpenApiDocument> GetDocument(string swaggerFile);
    }
}