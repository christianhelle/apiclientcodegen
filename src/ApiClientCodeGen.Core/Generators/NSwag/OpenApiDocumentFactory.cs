using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag
{
    public interface IOpenApiDocumentFactory
    {
        OpenApiDocument GetDocument(string swaggerFile);
    }
}