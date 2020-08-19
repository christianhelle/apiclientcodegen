using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public static class NSwagStudioFileHelper
    {
        public static async Task<string> CreateNSwagStudioFileAsync(
            EnterOpenApiSpecDialogResult enterOpenApiSpecDialogResult,
            INSwagStudioOptions options = null,
            string outputNamespace = null)
        {
            var specifications = enterOpenApiSpecDialogResult.OpenApiSpecification;
            var outputFilename = enterOpenApiSpecDialogResult.OutputFilename;
            var url = enterOpenApiSpecDialogResult.Url;
            var openApiDocument = url.EndsWith("yaml") || url.EndsWith("yml")
                ? await OpenApiYamlDocument.FromUrlAsync(url)
                : await OpenApiDocument.FromJsonAsync(specifications);
            var className = options?.UseDocumentTitle ?? true
                ? openApiDocument.GenerateClassName()
                : outputFilename;

            return new
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
                        ClassStyle = options?.ClassStyle ?? CSharpClassStyle.Poco,
                        GenerateDefaultValues = options?.GenerateDefaultValues ?? true,
                        GenerateDataAnnotations = options?.GenerateDataAnnotations ?? true,
                        Namespace = outputNamespace ?? "GeneratedCode",
                        Output = $"{className}.cs"
                    }
                }
            }
                .ToJson();
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
