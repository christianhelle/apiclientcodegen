using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Options.NSwagStudio;

namespace Rapicgen.Core.Generators.NSwagStudio
{
    public static class NSwagStudioFileHelper
    {
        public static Task<string> CreateNSwagStudioFileAsync(
            EnterOpenApiSpecDialogResult enterOpenApiSpecDialogResult,
            INSwagStudioOptions? options = null,
            string? outputNamespace = null)
        {
            var specifications = enterOpenApiSpecDialogResult.OpenApiSpecification;
            var outputFilename = enterOpenApiSpecDialogResult.OutputFilename;
            
            var className = options?.UseDocumentTitle ?? true
                ? GetClassNameFromContent(specifications, outputFilename)
                : outputFilename;

            return Task.FromResult(new
            {
                Runtime = "Default",
                SwaggerGenerator = new
                {
                    FromSwagger = GetFromSwagger(enterOpenApiSpecDialogResult, specifications)
                },
                CodeGenerators = new
                {
                    SwaggerToCSharpClient = new
                    {
                        ClassName = className,
                        InjectHttpClient = options?.InjectHttpClient ?? true,
                        GenerateClientInterfaces = options?.GenerateClientInterfaces ?? true,
                        GenerateDtoTypes = options?.GenerateDtoTypes ?? true,
                        UseBaseUrl = options?.UseBaseUrl ?? false,
                        OperationGenerationMode = "MultipleClientsFromOperationId",
                        GenerateResponseClasses = options?.GenerateResponseClasses ?? true,
                        GenerateJsonMethods = options?.GenerateJsonMethods ?? true,
                        RequiredPropertiesMustBeDefined = options?.RequiredPropertiesMustBeDefined ?? true,
                        ClassStyle = options?.ClassStyle ?? "Poco",
                        GenerateDefaultValues = options?.GenerateDefaultValues ?? true,
                        GenerateDataAnnotations = options?.GenerateDataAnnotations ?? true,
                        Namespace = outputNamespace ?? "GeneratedCode",
                        Output = $"{className}.cs"
                    }
                }
            }
                .ToJson());
        }

        private static string GetClassNameFromContent(string content, string fallback)
        {
             var titleMatch = Regex.Match(content, @"[""']?title[""']?:\s*[""']?([^""'\r\n]+)[""']?");
             if (titleMatch.Success)
             {
                 var title = titleMatch.Groups[1].Value.Trim();
                 return title.Replace("Swagger", "").Replace(" ", "").Replace(".", "").Replace("-", "") + "Client";
             }
             return fallback;
        }

        private static object GetFromSwagger(
            EnterOpenApiSpecDialogResult enterOpenApiSpecDialogResult,
            string specifications)
        {
            var url = enterOpenApiSpecDialogResult.Url;
            if (url.EndsWith("yaml"))
                return new
                {
                    Yaml = specifications,
                    Url = url
                };
            
            return new
            {
                Json = specifications,
                Url = url
            };
        }
    }
}
