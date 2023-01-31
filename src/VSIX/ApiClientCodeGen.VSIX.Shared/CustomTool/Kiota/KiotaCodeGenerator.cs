using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Converters;

namespace Rapicgen.CustomTool.Kiota
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class KiotaCodeGenerator : SingleFileCodeGenerator
    {
        protected KiotaCodeGenerator(
            SupportedLanguage language,
            ILanguageConverter? languageConverter = null)
            : base(SupportedCodeGenerator.Kiota, language)
        {
        }
    }
}
