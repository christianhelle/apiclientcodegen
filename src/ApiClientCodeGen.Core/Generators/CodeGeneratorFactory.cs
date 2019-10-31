using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators
{
    public interface ICodeGeneratorFactory
    {
        ICodeGenerator Create(
            string defaultNamespace,
            string inputFileContents,
            string inputFilePath,
            SupportedLanguage language,
            SupportedCodeGenerator generator);
    }
}