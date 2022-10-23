using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Rapicgen.CustomTool;

namespace Rapicgen.Tests.CustomTool
{
    [ExcludeFromCodeCoverage]
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
