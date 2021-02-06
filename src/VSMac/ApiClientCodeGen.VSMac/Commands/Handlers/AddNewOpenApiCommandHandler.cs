using ApiClientCodeGen.VSMac.CustomTools.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewOpenApiCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => OpenApiSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.OpenApi;
    }
}