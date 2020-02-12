using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.CustomTool
{
    internal class CSharpSingleFileCodeGenerator : SingleFileCodeGenerator
    {
        internal CSharpSingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator)
            : base(supportedCodeGenerator)
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}
