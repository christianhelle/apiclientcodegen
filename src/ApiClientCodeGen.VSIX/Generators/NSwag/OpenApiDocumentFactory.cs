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
                return ThreadHelper.JoinableTaskFactory?.Run(
                    () => swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                        ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                        : OpenApiDocument.FromFileAsync(swaggerFile));
            }
            catch (NullReferenceException)
            {
                return (swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                        ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                        : OpenApiDocument.FromFileAsync(swaggerFile))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
