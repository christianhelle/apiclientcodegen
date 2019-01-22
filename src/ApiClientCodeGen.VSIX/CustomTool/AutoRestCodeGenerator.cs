using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    [ComVisible(true)]
    public abstract class AutoRestCodeGenerator : CodeGenerator
    {
        protected AutoRestCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.AutoRest, language)
        {
        }
    }
}
