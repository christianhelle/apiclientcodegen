using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest
{
    [ComVisible(true)]
    public abstract class AutoRestCodeGenerator : SingleFileCodeGenerator
    {
        protected AutoRestCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.AutoRest, language)
        {
        }
    }
}
