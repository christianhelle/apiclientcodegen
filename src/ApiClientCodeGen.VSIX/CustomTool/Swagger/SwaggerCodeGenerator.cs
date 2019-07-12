using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Converters;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class SwaggerCodeGenerator : SingleFileCodeGenerator
    {
        protected SwaggerCodeGenerator(
            SupportedLanguage language, 
            ILanguageConverter converter = null)
            : base(SupportedCodeGenerator.Swagger, language)
        {
        }
    }
}
