using ApiClientCodeGen.VSMac.CustomTools.AutoRest;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateAutoRestCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName
            => AutoRestSingleFileCustomTool.GeneratorName;
    }
}
