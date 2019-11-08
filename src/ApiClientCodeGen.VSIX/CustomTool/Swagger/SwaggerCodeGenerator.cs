using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Converters;

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
