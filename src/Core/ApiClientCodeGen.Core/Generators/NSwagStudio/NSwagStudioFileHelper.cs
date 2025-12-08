using System.Threading.Tasks;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Options.NSwagStudio;
using NSwag;
using NJsonSchemaClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle;
using CSharpClassStyle = Rapicgen.Core.Options.NSwag.CSharpClassStyle;

namespace Rapicgen.Core.Generators.NSwagStudio
{
    public static class NSwagStudioFileHelper
    {
        public static async Task<string> CreateNSwagStudioFileAsync(
            EnterOpenApiSpecDialogResult enterOpenApiSpecDialogResult,
            INSwagStudioOptions? options = null,
            string? outputNamespace = null)
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
                        ClassStyle = ConvertClassStyle(options?.ClassStyle ?? CSharpClassStyle.Poco),
                        GenerateDefaultValues = options?.GenerateDefaultValues ?? true,
                        GenerateDataAnnotations = options?.GenerateDataAnnotations ?? true,
                        Namespace = outputNamespace ?? "GeneratedCode",
                        Output = $"{className}.cs"
                    }
                }
            }
                .ToJson();
        }

        private static NJsonSchemaClassStyle ConvertClassStyle(CSharpClassStyle classStyle)
            => classStyle switch
            {
                CSharpClassStyle.Poco => NJsonSchemaClassStyle.Poco,
                CSharpClassStyle.Inpc => NJsonSchemaClassStyle.Inpc,
                CSharpClassStyle.Prism => NJsonSchemaClassStyle.Prism,
                CSharpClassStyle.Record => NJsonSchemaClassStyle.Record,
                _ => NJsonSchemaClassStyle.Poco
            };

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
