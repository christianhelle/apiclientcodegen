using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Models;
using Microsoft.VisualStudio.Shell;

namespace Rapicgen.Generators.NSwag
{
    [ExcludeFromCodeCoverage]
    internal class OpenApiDocumentFactory : IOpenApiDocumentFactory
    {
        public Task<SimpleOpenApiDocument> GetDocumentAsync(string swaggerFile)
        {
            var document = new SimpleOpenApiDocument
            {
                DocumentPath = swaggerFile,
                Info = new SimpleOpenApiInfo()
            };

            try
            {
                if (File.Exists(swaggerFile))
                {
                    var content = File.ReadAllText(swaggerFile);

                    var openApiMatch = Regex.Match(content, @""[""']?openapi[""']?:\s*[""']?([\d\.]+)[""']?"");
                    if (openApiMatch.Success)
                    {
                        document.OpenApi = openApiMatch.Groups[1].Value;
                    }

                    var swaggerMatch = Regex.Match(content, @""[""']?swagger[""']?:\s*[""']?([\d\.]+)[""']?"");
                    if (swaggerMatch.Success)
                    {
                        document.Swagger = swaggerMatch.Groups[1].Value;
                    }

                    var titleMatch = Regex.Match(content, @""[""']?title[""']?:\s*[""']?([^""'\r\n]+)[""']?"");
                    if (titleMatch.Success)
                    {
                        document.Info.Title = titleMatch.Groups[1].Value.Trim();
                    }
                }
            }
            catch
            {
                // Ignore errors
            }

            return Task.FromResult(document);
        }
    }
}
