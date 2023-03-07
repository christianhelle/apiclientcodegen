using ApiClientCodeGen.VSMac.CustomTools.Kiota;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateKiotaCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName
            => KiotaSingleFileCustomTool.GeneratorName;
    }
}