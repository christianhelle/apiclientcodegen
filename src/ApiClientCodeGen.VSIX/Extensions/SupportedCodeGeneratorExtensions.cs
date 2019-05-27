using System.Collections.Generic;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.NuGet;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class SupportedCodeGeneratorExtensions
    {
        private static readonly PackageDependencyListProvider dependencyListProvider
            = new PackageDependencyListProvider();

        public static string GetCustomToolName(this SupportedCodeGenerator generator)
        {
            string customTool;
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
                default:
                    customTool = nameof(NSwagCodeGenerator);
                    break;
            }

            return customTool;
        }

        public static IEnumerable<PackageDependency> GetDependencies(
            this SupportedCodeGenerator generator)
            => dependencyListProvider
                .GetDependencies(generator);
    }
}