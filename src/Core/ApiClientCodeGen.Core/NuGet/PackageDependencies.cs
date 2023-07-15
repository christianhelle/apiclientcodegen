using System;

namespace Rapicgen.Core.NuGet
{
    public static class PackageDependencies
    {
        public static readonly PackageDependency NewtonsoftJson =
            new PackageDependency(
                "Newtonsoft.Json",
                new Version(13, 0, 3, 0),
                false);

        public static readonly PackageDependency MicrosoftRestClientRuntime =
            new PackageDependency(
                "Microsoft.Rest.ClientRuntime",
                new Version(2, 3, 24, 0));

        public static readonly PackageDependency RestSharp =
            new PackageDependency(
                "RestSharp",
                new Version(105, 1, 0, 0));

        public static readonly PackageDependency JsonSubTypes =
            new PackageDependency(
                "JsonSubTypes",
                new Version(1, 8, 0, 0));

        public static readonly PackageDependency RestSharpLatest =
            new PackageDependency(
                "RestSharp",
                new Version(108, 0, 2, 0));

        public static readonly PackageDependency JsonSubTypesLatest =
            new PackageDependency(
                "JsonSubTypes",
                new Version(2, 0, 1, 0));

        public static readonly PackageDependency SystemRuntimeSerializationPrimitives =
            new PackageDependency(
                "System.Runtime.Serialization.Primitives",
                new Version(4, 3, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency SystemComponentModelAnnotations =
            new PackageDependency(
                "System.ComponentModel.Annotations",
                new Version(5, 0, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency MicrosoftCSharp =
            new PackageDependency(
                "Microsoft.CSharp",
                new Version(4, 5, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency Polly =
            new PackageDependency(
                "Polly",
                "7.2.4");

        public static readonly PackageDependency AutoRestCSharp =
            new PackageDependency(
                "Microsoft.Azure.AutoRest.CSharp",
                "3.0.0-beta.20210218.1");

        public static readonly PackageDependency AzureCore =
            new PackageDependency(
                "Azure.Core",
                new Version(1, 34, 0));

        public static readonly PackageDependency AzureIdentity =
            new PackageDependency(
                "Azure.Identity",
                new Version(1, 8, 2));

        public static readonly PackageDependency MicrosoftKiotaAbstractions =
            new PackageDependency(
                "Microsoft.Kiota.Abstractions",
                "1.2.1");

        public static readonly PackageDependency MicrosoftKiotaAuthenticationAzure =
            new PackageDependency(
                "Microsoft.Kiota.Authentication.Azure",
                "1.0.3");

        public static readonly PackageDependency MicrosoftKiotaHttpClientLibrary =
            new PackageDependency(
                "Microsoft.Kiota.Http.HttpClientLibrary",
                "1.0.6");

        public static readonly PackageDependency MicrosoftKiotaSerializationForm =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Form",
                "1.0.1");

        public static readonly PackageDependency MicrosoftKiotaSerializationJson =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Json",
                "1.0.8");

        public static readonly PackageDependency MicrosoftKiotaSerializationText =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Text",
                "1.0.3");

        public static readonly PackageDependency Refit =
            new PackageDependency(
                "Refit",
                new Version(7, 0, 0));
    }
}