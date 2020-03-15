using ApiClientCodeGen.VSMac.CustomTools.NSwag;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName
            => NSwagSingleFileCustomTool.GeneratorName;
    }
}