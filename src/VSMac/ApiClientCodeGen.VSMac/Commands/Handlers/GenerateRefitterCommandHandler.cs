using ApiClientCodeGen.VSMac.CustomTools.Refitter;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateRefitterCommandHandler : GenerateCommandHandler
    {
        protected override string GeneratorName
            => RefitterSingleFileCustomTool.GeneratorName;
    }
}