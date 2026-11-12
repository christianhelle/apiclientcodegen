using System.Collections.Generic;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.NuGet
{
    public class PackageDependencyListProvider
    {
        public IEnumerable<PackageDependency> GetDependencies(
            OpenApiSupportedVersion openApiGeneratorVersion)
        {
            if (openApiGeneratorVersion.IsAtLeast(OpenApiSupportedVersion.V7120))
            {
                return GetDependencies(SupportedCodeGenerator.OpenApi);
            }

            return
            [
                PackageDependencies.Polly,
                PackageDependencies.RestSharpLatest,
                PackageDependencies.JsonSubTypesLatest,
                PackageDependencies.NewtonsoftJson,
                PackageDependencies.SystemRuntimeSerializationPrimitives,
                PackageDependencies.SystemComponentModelAnnotations,
                PackageDependencies.MicrosoftCSharp
            ];
        }

        public IEnumerable<PackageDependency> GetDependencies(
            SupportedCodeGenerator generator)
            => GetDependencies(generator, System.Array.Empty<string>());

        public IEnumerable<PackageDependency> GetDependencies(
            SupportedCodeGenerator generator,
            IEnumerable<string> targetFrameworks)
        {
            var list = new List<PackageDependency>();
            switch (generator)
            {
                case SupportedCodeGenerator.NSwag:
                case SupportedCodeGenerator.NSwagStudio:
                    list.AddRange(
                        new[]
                        {
                            PackageDependencies.NewtonsoftJson,
                            PackageDependencies.SystemRuntimeSerializationPrimitives,
                            PackageDependencies.SystemComponentModelAnnotations
                        });
                    break;

                case SupportedCodeGenerator.Swagger:
                    list.AddRange(
                        new[]
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
                    list.AddRange(
                        new[]
                        {
                            PackageDependencies.MicrosoftExtensionsHttp,
                            PackageDependencies.MicrosoftExtensionsHosting,
                            PackageDependencies.MicrosoftExtensionsHttpPolly,
                            PackageDependencies.SystemThreadingChannels,
                            PackageDependencies.SystemComponentModelAnnotations,
                        });
                    break;

                case SupportedCodeGenerator.Kiota:
                    list.AddRange(
                        new[]
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
                    list.AddRange(
                        new[]
                        {
                            ProjectTargetFrameworks.AreSupportedByLatestRefit(targetFrameworks)
                                ? PackageDependencies.Refit
                                : PackageDependencies.RefitNetStandard,
                            PackageDependencies.SystemRuntimeSerializationPrimitives,
                            PackageDependencies.SystemComponentModelAnnotations
                        });
                    break;
            }

            return list;
        }
    }
}