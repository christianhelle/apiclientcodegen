using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Converters;

namespace Rapicgen.CustomTool.Refitter
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class RefitterCodeGenerator : SingleFileCodeGenerator
    {
        protected RefitterCodeGenerator(
            SupportedLanguage language,
            ILanguageConverter? languageConverter = null)
            : base(SupportedCodeGenerator.Refitter, language)
        {
        }
    }
}
