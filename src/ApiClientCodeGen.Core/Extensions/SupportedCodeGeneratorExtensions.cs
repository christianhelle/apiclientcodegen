using System;
using System.Collections.Generic;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class SupportedCodeGeneratorExtensions
    {
        private static readonly PackageDependencyListProvider DependencyListProvider
            = new PackageDependencyListProvider();

        public static IEnumerable<PackageDependency> GetDependencies(
            this SupportedCodeGenerator generator)
            => DependencyListProvider
                .GetDependencies(generator);

        public static string GetName(this SupportedCodeGenerator generator)
        {
            switch (generator)
            {
                case SupportedCodeGenerator.Swagger:
                    return "Swagger Codegen CLI";
                case SupportedCodeGenerator.OpenApi:
                    return "OpenAPI Generator";
                default:
                    return generator.ToString();
            }
        }
    }
}