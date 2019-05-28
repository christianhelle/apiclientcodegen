using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public sealed class NSwagStudioFileHelper
    {
        public static async Task<string> CreateNSwagStudioFileAsync(string openApiSpec, string openApiSpecUrl)
        {
            var className = (await SwaggerDocument.FromJsonAsync(openApiSpec)).GenerateClassName();
            return new
            {
                Runtime = "Default",
                SwaggerGenerator = new
                {
                    FromSwagger = new
                    {
                        Json = openApiSpec,
                        Url = openApiSpecUrl
                    }
                },
                CodeGenerators = new
                {
                    SwaggerToCSharpClient = new
                    {
                        ClassName = className,
                        InjectHttpClient = true,
                        GenerateClientInterfaces = true,
                        GenerateDtoTypes = true,
                        UseBaseUrl = false,
                        OperationGenerationMode = "MultipleClientsFromOperationId",
                        GenerateResponseClasses = true,
                        GenerateJsonMethods = true,
                        RequiredPropertiesMustBeDefined = true,
                        classStyle = "Inpc",
                        GenerateDefaultValues = true,
                        GenerateDataAnnotations = true,
                        Output = $"{className}.cs"
                    }
                }
            }
                .ToJson();
        }
    }
}
