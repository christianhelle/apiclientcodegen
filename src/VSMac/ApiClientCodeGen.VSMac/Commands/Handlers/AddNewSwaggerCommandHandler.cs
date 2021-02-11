using ApiClientCodeGen.VSMac.CustomTools.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewSwaggerCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => SwaggerSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.Swagger;
    }
}