using System.Threading.Tasks;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Options.NSwagStudio;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;

namespace ApiClientCodeGen.VSMac.Commands.NSwagStudio
{
    public static class NSwagStudioFileHelper
    {
        public static async Task<string> CreateNSwagStudioFileAsync(
            string swaggerJson,
            string url,
            INSwagStudioOptions options = null,
            string outputNamespace = null)
        {
            var openApiDocument = await OpenApiDocument.FromJsonAsync(swaggerJson);
            var className = options?.UseDocumentTitle ?? true ? openApiDocument.GenerateClassName() : "GeneratedCode.cs";
            return new
                {
                    Runtime = "Default",
                    SwaggerGenerator = new
                    {
                        FromSwagger = new
                        {
                            Json = swaggerJson,
                            url
                        }
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
    }
}