using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Converters;

namespace Rapicgen.CustomTool.NSwag
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class NSwagCodeGenerator : SingleFileCodeGenerator
    {
        protected NSwagCodeGenerator(
            SupportedLanguage language,
            ILanguageConverter? converter = null)
            : base(SupportedCodeGenerator.NSwag, language, converter)
        {
        }
    }
}
