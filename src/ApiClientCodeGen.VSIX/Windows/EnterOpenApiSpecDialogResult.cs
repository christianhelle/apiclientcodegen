using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    public class EnterOpenApiSpecDialogResult
    {
        public EnterOpenApiSpecDialogResult(
            string openApiSpecification,
            SupportedCodeGenerator selectedCodeGenerator,
            string outputFilename)
        {
            OpenApiSpecification = openApiSpecification;
            SelectedCodeGenerator = selectedCodeGenerator;
            OutputFilename = outputFilename;
        }

        public string OpenApiSpecification { get; }
        public SupportedCodeGenerator SelectedCodeGenerator { get; }
        public string OutputFilename { get; }
    }
}