namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    public class EnterOpenApiSpecDialogResult
    {
        public EnterOpenApiSpecDialogResult(
            string openApiSpecification,
            string outputFilename,
            string url)
        {
            OpenApiSpecification = openApiSpecification;
            OutputFilename = outputFilename;
            Url = url;
        }

        public string Url { get; set; }
        public string OpenApiSpecification { get; }
        public string OutputFilename { get; }
    }
}