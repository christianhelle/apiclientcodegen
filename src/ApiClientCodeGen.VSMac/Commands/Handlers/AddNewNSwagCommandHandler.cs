using ApiClientCodeGen.VSMac.CustomTools.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewNSwagCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName
            => NSwagSingleFileCustomTool.GeneratorName;

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.NSwag;

        protected override bool SupportsYaml => false;
    }
}