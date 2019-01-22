using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string className,
            string defaultNamespace,
            string inputFileContents,
            string inputFilePath,
            SupportedLanguage language,
            SupportedCodeGenerator generator)
        {
            switch (generator)
            {
                case SupportedCodeGenerator.AutoRest:
                    if (language == SupportedLanguage.CSharp)
                        return new AutoRestCSharpGenerator(inputFilePath, defaultNamespace);
                    break;
            }

            throw new NotSupportedException();
        }
    }
}
