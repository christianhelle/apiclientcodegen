namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
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