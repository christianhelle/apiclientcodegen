using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using Microsoft.VisualStudio.Shell;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public OpenApiDocument GetDocument(string swaggerFile)
        {
            try
            {
                return ThreadHelper.JoinableTaskFactory
                    ?.Run(() => OpenApiDocument.FromFileAsync(swaggerFile));
            }
            catch (NullReferenceException)
            {
                return OpenApiDocument
                    .FromFileAsync(swaggerFile)
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
