using ApiClientCodeGen.VSMac.CustomTools.Swagger;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateSwaggerCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName 
            => SwaggerSingleFileCustomTool.GeneratorName;
    }
}