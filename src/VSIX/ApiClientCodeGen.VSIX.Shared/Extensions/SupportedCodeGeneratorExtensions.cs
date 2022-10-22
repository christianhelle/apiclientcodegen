using System;
using Rapicgen.Core;
using Rapicgen.CustomTool.AutoRest;
using Rapicgen.CustomTool.NSwag;
using Rapicgen.CustomTool.OpenApi;
using Rapicgen.CustomTool.Swagger;

namespace Rapicgen.Extensions
{
    public static class SupportedCodeGeneratorExtensions
    {
        public static string? GetCustomToolName(this SupportedCodeGenerator generator)
        {
            string? customTool = null;
            switch (generator)
            {
                case SupportedCodeGenerator.NSwag:
                    customTool = nameof(NSwagCodeGenerator);
                    break;
                case SupportedCodeGenerator.AutoRest:
                    customTool = nameof(AutoRestCodeGenerator);
                    break;
                case SupportedCodeGenerator.Swagger:
                    customTool = nameof(SwaggerCodeGenerator);
                    break;
                case SupportedCodeGenerator.OpenApi:
                    customTool = nameof(OpenApiCodeGenerator);
                    break;
            }

            return customTool;
        }

        public static SupportedCodeGenerator GetSupportedCodeGenerator(this Type type)
        {
            if (type.IsAssignableFrom(typeof(NSwagCodeGenerator)))
                return SupportedCodeGenerator.NSwag;

            if (type.IsAssignableFrom(typeof(AutoRestCodeGenerator)))
                return SupportedCodeGenerator.AutoRest;

            if (type.IsAssignableFrom(typeof(SwaggerCodeGenerator)))
                return SupportedCodeGenerator.Swagger;

            if (type.IsAssignableFrom(typeof(OpenApiCodeGenerator)))
                return SupportedCodeGenerator.OpenApi;

            throw new NotSupportedException();
        }
    }
}