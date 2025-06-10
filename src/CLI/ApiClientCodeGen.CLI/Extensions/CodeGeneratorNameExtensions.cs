using Rapicgen.CLI.Commands;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;

namespace Rapicgen.CLI.Extensions
{
    public static class CodeGeneratorNameExtensions
    {
        public static string GetCodeGeneratorName(this object generator)
        {
            var type = generator.GetType();
            
            if (type == typeof(OpenApiCSharpGeneratorCommand))
                return SupportedCodeGenerator.OpenApi.GetName();
            
            if (type == typeof(SwaggerCodegenCommand))
                return SupportedCodeGenerator.Swagger.GetName();
            
            return type.Name.Replace("Command", string.Empty);
        }
    }
}