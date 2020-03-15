using ApiClientCodeGen.VSMac.CustomTools.OpenApi;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateOpenApiCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName
            => OpenApiSingleFileCustomTool.GeneratorName;
    }
}