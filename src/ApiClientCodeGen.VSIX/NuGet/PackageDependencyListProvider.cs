
using System;
using System.Collections.Generic;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.NuGet
{
    public class PackageDependencyListProvider
    {
        public IEnumerable<PackageDependency> GetDependencies(
            SupportedCodeGenerator generator)
        {
            switch (generator)
            {
                case SupportedCodeGenerator.NSwag:
                case SupportedCodeGenerator.NSwagStudio:
                    yield return new PackageDependency(
                        "Newtonsoft.Json",
                        new Version(12, 0, 1, 0));
                    break;

                case SupportedCodeGenerator.AutoRest:
                    yield return new PackageDependency(
                        "Microsoft.Rest.ClientRuntime",
                        new Version(2, 3, 20, 0));
                    break;

                case SupportedCodeGenerator.Swagger:
                case SupportedCodeGenerator.OpenApi:
                    yield return new PackageDependency(
                        "RestSharp",
                        new Version(105, 1, 0, 0));
                    yield return new PackageDependency(
                        "JsonSubTypes",
                        new Version(1, 2, 0, 0));
                    break;
            }
        }
    }
}
