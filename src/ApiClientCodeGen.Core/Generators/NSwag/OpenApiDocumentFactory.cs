using System;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public interface IOpenApiDocumentFactory
    {
        OpenApiDocument GetDocument(string swaggerFile);
    }
}