using ApiClientCodeGen.Core;

namespace ApiClientCodeGen
{
    public abstract class AutoRestCodeGenerator : CodeGenerator
    {
        protected AutoRestCodeGenerator(SupportedLanguage language)
            : base(SupportedCodeGenerator.AutoRest, language)
        {
        }
    }
}
