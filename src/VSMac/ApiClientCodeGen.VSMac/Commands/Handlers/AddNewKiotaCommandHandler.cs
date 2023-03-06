using ApiClientCodeGen.VSMac.CustomTools.Kiota;
using Rapicgen.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewKiotaCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => KiotaSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.Kiota;
    }
}