using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    public class EnterOpenApiSpecDialogResult
    {
        public EnterOpenApiSpecDialogResult(
            string openApiSpecification,
            SupportedCodeGenerator selectedCodeGenerator,
            string outputFilename,
            string url)
        {
            OpenApiSpecification = openApiSpecification;
            SelectedCodeGenerator = selectedCodeGenerator;
            OutputFilename = outputFilename;
            Url = url;
        }

        public string Url { get; set; }
        public string OpenApiSpecification { get; }
        public SupportedCodeGenerator SelectedCodeGenerator { get; }
        public string OutputFilename { get; }
    }
}