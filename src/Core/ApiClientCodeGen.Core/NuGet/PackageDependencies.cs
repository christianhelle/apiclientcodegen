using System;

namespace Rapicgen.Core.NuGet
{
    public static class PackageDependencies
    {
        public static readonly PackageDependency NewtonsoftJson =
            new PackageDependency(
                "Newtonsoft.Json",
                "13.0.3",
                false);

        public static readonly PackageDependency MicrosoftRestClientRuntime =
            new PackageDependency(
                "Microsoft.Rest.ClientRuntime",
                "2.3.24");

        public static readonly PackageDependency RestSharp =
            new PackageDependency(
                "RestSharp",
                "105.1.0");

        public static readonly PackageDependency JsonSubTypes =
            new PackageDependency(
                "JsonSubTypes",
                "1.8.0");

        public static readonly PackageDependency RestSharpLatest =
            new PackageDependency(
                "RestSharp",
                "110.2.0");

        public static readonly PackageDependency JsonSubTypesLatest =
            new PackageDependency(
                "JsonSubTypes",
                "2.0.1");

        public static readonly PackageDependency SystemRuntimeSerializationPrimitives =
            new PackageDependency(
                "System.Runtime.Serialization.Primitives",
                "4.3.0",
                isSystemLibrary: true);

        public static readonly PackageDependency SystemComponentModelAnnotations =
            new PackageDependency(
                "System.ComponentModel.Annotations",
                "5.0.0",
                isSystemLibrary: true);

        public static readonly PackageDependency MicrosoftCSharp =
            new PackageDependency(
                "Microsoft.CSharp",
                "4.5.0",
                isSystemLibrary: true);

        public static readonly PackageDependency Polly =
            new PackageDependency(
                "Polly",
                "8.4.0");

        public static readonly PackageDependency AutoRestCSharp =
            new PackageDependency(
                "Microsoft.Azure.AutoRest.CSharp",
                "3.0.0-beta.20210218.1");

        public static readonly PackageDependency AzureCore =
            new PackageDependency(
                "Azure.Core",
                "1.36.0");

        public static readonly PackageDependency AzureIdentity =
            new PackageDependency(
                "Azure.Identity",
                "1.11.4");

        public static readonly PackageDependency MicrosoftKiotaAbstractions =
            new PackageDependency(
                "Microsoft.Kiota.Abstractions",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaAuthenticationAzure =
            new PackageDependency(
                "Microsoft.Kiota.Authentication.Azure",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaHttpClientLibrary =
            new PackageDependency(
                "Microsoft.Kiota.Http.HttpClientLibrary",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaSerializationForm =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Form",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaSerializationJson =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Json",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaSerializationText =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Text",
                "1.12.4");

        public static readonly PackageDependency MicrosoftKiotaSerializationMultipart =
            new PackageDependency(
                "Microsoft.Kiota.Serialization.Multipart",
                "1.12.4");

        public static readonly PackageDependency Refit =
            new PackageDependency(
                "Refit",
                "7.0.0");
    }
}