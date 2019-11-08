using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class AutoRestCodeGenerator : SingleFileCodeGenerator
    {
        protected AutoRestCodeGenerator(SupportedLanguage language, ILanguageConverter languageConverter = null)
            : base(SupportedCodeGenerator.AutoRest, language, languageConverter)
        {
        }
    }
}
