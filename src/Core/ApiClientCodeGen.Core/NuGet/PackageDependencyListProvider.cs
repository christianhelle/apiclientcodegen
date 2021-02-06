using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet
{
    public class PackageDependencyListProvider
    {
        public IEnumerable<PackageDependency> GetDependencies(
            SupportedCodeGenerator generator)
        {
            var list = new List<PackageDependency>();
            switch (generator)
            {
                case SupportedCodeGenerator.NSwag:
                case SupportedCodeGenerator.NSwagStudio:
                    list.AddRange(new[]
                    {
                        PackageDependencies.NewtonsoftJson,
                        PackageDependencies.SystemRuntimeSerializationPrimitives,
                        PackageDependencies.SystemComponentModelAnnotations
                    });
                    break;

                case SupportedCodeGenerator.AutoRest:
                    list.AddRange(new[]
                    {
                        PackageDependencies.MicrosoftRestClientRuntime,
                        PackageDependencies.NewtonsoftJson
                    });
                    break;

                case SupportedCodeGenerator.Swagger:
                    list.AddRange(new[]
                    {
                        PackageDependencies.RestSharp,
                        PackageDependencies.JsonSubTypes,
                        PackageDependencies.NewtonsoftJson,
                        PackageDependencies.SystemRuntimeSerializationPrimitives,
                        PackageDependencies.SystemComponentModelAnnotations,
                        PackageDependencies.MicrosoftCSharp
                    });
                    break;

                case SupportedCodeGenerator.OpenApi:
                    list.AddRange(new[]
                    {
                        PackageDependencies.Polly,
                        PackageDependencies.RestSharpLatest,
                        PackageDependencies.JsonSubTypesLatest,
                        PackageDependencies.NewtonsoftJson,
                        PackageDependencies.SystemRuntimeSerializationPrimitives,
                        PackageDependencies.SystemComponentModelAnnotations,
                        PackageDependencies.MicrosoftCSharp
                    });
                    break;
            }
            return list;
        }
    }
}