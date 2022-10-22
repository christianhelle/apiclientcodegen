using Rapicgen.Converters;
using Rapicgen.Core;
using Rapicgen.CustomTool;
using Rapicgen.IntegrationTests.Utility;

namespace Rapicgen.IntegrationTests.CustomTool
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
