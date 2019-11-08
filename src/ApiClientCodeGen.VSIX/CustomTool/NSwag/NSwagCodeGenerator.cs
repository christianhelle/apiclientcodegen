using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class NSwagCodeGenerator : SingleFileCodeGenerator
    {
        protected NSwagCodeGenerator(
            SupportedLanguage language,
            ILanguageConverter converter = null)
            : base(SupportedCodeGenerator.NSwag, language, converter)
        {
        }
    }
}
