using ApiClientCodeGen.VSMac.CustomTools.OpenApi;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewOpenApiCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => OpenApiSingleFileCustomTool.GeneratorName;
    }
}