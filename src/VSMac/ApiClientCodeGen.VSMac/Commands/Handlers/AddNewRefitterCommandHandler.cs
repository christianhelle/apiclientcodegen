using ApiClientCodeGen.VSMac.CustomTools.Refitter;
using Rapicgen.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewRefitterCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => RefitterSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.Refitter;
    }
}