using ApiClientCodeGen.VSMac.CustomTools.AutoRest;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewAutoRestCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => AutoRestSingleFileCustomTool.GeneratorName;
    }
}
