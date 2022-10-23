using ApiClientCodeGen.VSMac.CustomTools.NSwag;
using Rapicgen.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewNSwagCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => NSwagSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.NSwag;
    }
}