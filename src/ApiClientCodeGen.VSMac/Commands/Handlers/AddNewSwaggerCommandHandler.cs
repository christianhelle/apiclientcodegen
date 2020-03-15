using ApiClientCodeGen.VSMac.CustomTools.Swagger;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewSwaggerCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => SwaggerSingleFileCustomTool.GeneratorName;
    }
}