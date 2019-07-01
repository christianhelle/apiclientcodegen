using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag
{
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
