using ApiClientCodeGen.VSMac.CustomTools.NSwag;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewNSwagCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => NSwagSingleFileCustomTool.GeneratorName;
    }
}