using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.CustomTool
{
    internal class TestSingleFileCodeGenerator : SingleFileCodeGenerator
    {
        internal TestSingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator)
            : base(supportedCodeGenerator, SupportedLanguage.CSharp)
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}
