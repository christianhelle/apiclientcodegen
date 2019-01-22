using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag
{
    [ComVisible(true)]
    public abstract class NSwagCodeGenerator : SingleFileCodeGenerator
    {
        protected NSwagCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.NSwag, language)
        {
        }
    }
}
