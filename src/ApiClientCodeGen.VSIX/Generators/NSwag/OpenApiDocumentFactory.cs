using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using Microsoft.VisualStudio.Shell;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    [ExcludeFromCodeCoverage]
    internal class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public async Task<OpenApiDocument> GetDocument(string swaggerFile)
        {
            try
            {
                return await ThreadHelper.JoinableTaskFactory.RunAsync(
                    () => swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                        ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                        : OpenApiDocument.FromFileAsync(swaggerFile));
            }
            catch (NullReferenceException)
            {
                return await (swaggerFile.EndsWith("yaml") || swaggerFile.EndsWith("yml")
                        ? OpenApiYamlDocument.FromFileAsync(swaggerFile)
                        : OpenApiDocument.FromFileAsync(swaggerFile));
            }
        }
    }
}
