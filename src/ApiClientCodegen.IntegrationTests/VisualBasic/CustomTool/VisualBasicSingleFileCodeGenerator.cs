using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.VisualBasic.CustomTool
{
    internal class VisualBasicSingleFileCodeGenerator : SingleFileCodeGenerator
    {
        internal VisualBasicSingleFileCodeGenerator(
            SupportedCodeGenerator supportedCodeGenerator)
            : base(
                supportedCodeGenerator, 
                SupportedLanguage.VisualBasic, 
                new CSharpToVisualBasicLanguageConverter())
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".vb";
            return 0;
        }
    }
}
