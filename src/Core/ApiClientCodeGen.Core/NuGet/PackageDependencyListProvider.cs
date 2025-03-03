using System.Collections.Generic;

namespace Rapicgen.Core.NuGet
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
                        PackageDependencies.NewtonsoftJson,
                    });
                    break;

                case SupportedCodeGenerator.AutoRestV3:
                    list.AddRange(new[]
                    {
                        PackageDependencies.MicrosoftRestClientRuntime,
                        PackageDependencies.NewtonsoftJson,
                        PackageDependencies.AutoRestCSharp,
                        PackageDependencies.AzureCore,
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
                        PackageDependencies.MicrosoftExtensionsHttp,
                        PackageDependencies.MicrosoftExtensionsHosting,
                        PackageDependencies.MicrosoftExtensionsHttpPolly,
                        PackageDependencies.SystemThreadingChannels,
                        PackageDependencies.SystemComponentModelAnnotations,
                    });
                    break;

                case SupportedCodeGenerator.Kiota:
                    list.AddRange(new[]
                    {
                        PackageDependencies.AzureIdentity,
                        PackageDependencies.MicrosoftKiotaAbstractions,
                        PackageDependencies.MicrosoftKiotaAuthenticationAzure,
                        PackageDependencies.MicrosoftKiotaHttpClientLibrary,
                        PackageDependencies.MicrosoftKiotaSerializationForm,
                        PackageDependencies.MicrosoftKiotaSerializationJson,
                        PackageDependencies.MicrosoftKiotaSerializationText,
                        PackageDependencies.MicrosoftKiotaSerializationMultipart,
                    });
                    break;
                
                case SupportedCodeGenerator.Refitter:
                    list.AddRange(new[]
                    {
                        PackageDependencies.Refit,
                        PackageDependencies.SystemRuntimeSerializationPrimitives,
                        PackageDependencies.SystemComponentModelAnnotations
                    });
                    break;
            }
            return list;
        }
    }
}