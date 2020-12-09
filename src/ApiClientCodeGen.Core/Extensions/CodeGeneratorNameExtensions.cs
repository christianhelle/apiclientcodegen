using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;

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

        public static string GetCodeGeneratorName(this CodeGeneratorCommand generator)
        {
            var type = generator.GetType();
            
            if (type == typeof(AutoRestCommand))
                return SupportedCodeGenerator.AutoRest.GetName();
            
            if (type == typeof(NSwagCommand))
                return SupportedCodeGenerator.NSwag.GetName();
            
            if (type == typeof(OpenApiGeneratorCommand))
                return SupportedCodeGenerator.OpenApi.GetName();
            
            if (type == typeof(SwaggerCodegenCommand))
                return SupportedCodeGenerator.Swagger.GetName();
            
            return type.Name.Replace("Command", string.Empty);
        }
    }
}