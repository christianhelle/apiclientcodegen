using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Converters;

namespace Rapicgen.CustomTool.OpenApi
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class OpenApiCodeGenerator : SingleFileCodeGenerator
    {
        protected OpenApiCodeGenerator(
            SupportedLanguage language, 
            ILanguageConverter? languageConverter = null)
            : base(SupportedCodeGenerator.OpenApi, language)
        {
        }
    }
}
