using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Converters;

namespace Rapicgen.CustomTool.Swagger
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public abstract class SwaggerCodeGenerator : SingleFileCodeGenerator
    {
        protected SwaggerCodeGenerator(
            SupportedLanguage language, 
            ILanguageConverter? converter = null)
            : base(SupportedCodeGenerator.Swagger, language)
        {
        }
    }
}
