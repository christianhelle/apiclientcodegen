using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    public abstract class AutoRestCodeGenerator : CodeGenerator
    {
        protected AutoRestCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.AutoRest, language)
        {
        }
    }
}
