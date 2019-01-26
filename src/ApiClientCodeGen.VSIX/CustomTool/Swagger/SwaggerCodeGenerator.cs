using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger
{
    [ComVisible(true)]
    public abstract class SwaggerCodeGenerator : SingleFileCodeGenerator
    {
        protected SwaggerCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.Swagger, language)
        {
        }
    }
}
