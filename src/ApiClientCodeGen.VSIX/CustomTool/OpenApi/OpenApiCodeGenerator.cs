using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class OpenApiCodeGenerator : SingleFileCodeGenerator
    {
        protected OpenApiCodeGenerator(
            SupportedLanguage language, 
            ILanguageConverter languageConverter = null)
            : base(SupportedCodeGenerator.OpenApi, language)
        {
        }
    }
}
