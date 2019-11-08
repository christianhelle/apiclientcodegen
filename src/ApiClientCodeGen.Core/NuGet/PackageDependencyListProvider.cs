using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet
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
                    yield return PackageDependencies.NewtonsoftJson;
                    yield return PackageDependencies.SystemRuntimeSerializationPrimitives;
                    yield return PackageDependencies.SystemComponentModelAnnotations;
                    break;

                case SupportedCodeGenerator.AutoRest:
                    yield return PackageDependencies.MicrosoftRestClientRuntime;
                    yield return PackageDependencies.NewtonsoftJson;
                    break;

                case SupportedCodeGenerator.Swagger:
                case SupportedCodeGenerator.OpenApi:
                    yield return PackageDependencies.RestSharp;
                    yield return PackageDependencies.JsonSubTypes;
                    yield return PackageDependencies.NewtonsoftJson;
                    yield return PackageDependencies.SystemRuntimeSerializationPrimitives;
                    yield return PackageDependencies.SystemComponentModelAnnotations;
                    yield return PackageDependencies.MicrosoftCSharp;
                    break;
            }
        }
    }
}