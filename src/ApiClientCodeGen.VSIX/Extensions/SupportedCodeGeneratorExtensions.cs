using System;
using System.Collections.Generic;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class SupportedCodeGeneratorExtensions
    {
        private static readonly PackageDependencyListProvider DependencyListProvider
            = new PackageDependencyListProvider();

        public static string GetCustomToolName(this SupportedCodeGenerator generator)
        {
            string customTool = null;
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

        public static IEnumerable<PackageDependency> GetDependencies(
            this SupportedCodeGenerator generator)
            => DependencyListProvider
                .GetDependencies(generator);
    }
}