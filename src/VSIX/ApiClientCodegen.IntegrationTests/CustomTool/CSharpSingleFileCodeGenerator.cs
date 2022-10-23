using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Rapicgen.CustomTool;

namespace Rapicgen.IntegrationTests.CustomTool
{
    [ExcludeFromCodeCoverage]
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
