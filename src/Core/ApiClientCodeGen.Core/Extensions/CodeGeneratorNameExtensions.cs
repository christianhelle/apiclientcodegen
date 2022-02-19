namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class CodeGeneratorNameExtensions
    {
        public static string GetName(this SupportedCodeGenerator generator)
        {
            switch (generator)
            {
                case SupportedCodeGenerator.Swagger:
                    return "Swagger Codegen CLI";
                case SupportedCodeGenerator.OpenApi:
                    return "OpenAPI Generator";
                case SupportedCodeGenerator.NSwagStudio:
                    return "NSwag Studio";
                default:
                    return generator.ToString();
            }
        }
    }
}