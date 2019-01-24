using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(
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

                case SupportedCodeGenerator.NSwag:
                    if (language == SupportedLanguage.CSharp)
                        return new NSwagCSharpCodeGenerator(inputFilePath, defaultNamespace);
                    break;

                case SupportedCodeGenerator.Swagger:
                    if (language == SupportedLanguage.CSharp)
                        return new SwaggerCSharpCodeGenerator(inputFileContents, defaultNamespace);
                    break;
            }

            throw new NotSupportedException();
        }
    }
}
