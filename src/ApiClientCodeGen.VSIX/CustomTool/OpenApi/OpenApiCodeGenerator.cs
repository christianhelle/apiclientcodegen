using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi
{
    [ComVisible(true)]
    public abstract class OpenApiCodeGenerator : SingleFileCodeGenerator
    {
        protected OpenApiCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.OpenApi, language)
        {
        }
    }
}
