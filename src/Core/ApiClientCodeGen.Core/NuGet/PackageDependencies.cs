using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet
{
    public static class PackageDependencies
    {
        public static readonly PackageDependency NewtonsoftJson =
            new PackageDependency(
                "Newtonsoft.Json",
                new Version(13, 0, 1, 0),
                false);

        public static readonly PackageDependency MicrosoftRestClientRuntime =
            new PackageDependency(
                "Microsoft.Rest.ClientRuntime",
                new Version(2, 3, 23, 0));

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
                new Version(108, 0, 1, 0));

        public static readonly PackageDependency JsonSubTypesLatest =
            new PackageDependency(
                "JsonSubTypes",
                new Version(1, 9, 0, 0));

        public static readonly PackageDependency SystemRuntimeSerializationPrimitives =
            new PackageDependency(
                "System.Runtime.Serialization.Primitives",
                new Version(4, 3, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency SystemComponentModelAnnotations =
            new PackageDependency(
                "System.ComponentModel.Annotations",
                new Version(4, 5, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency MicrosoftCSharp =
            new PackageDependency(
                "Microsoft.CSharp",
                new Version(4, 5, 0),
                isSystemLibrary: true);

        public static readonly PackageDependency Polly =
            new PackageDependency(
                "Polly",
                new Version(7, 2, 3));

        public static readonly PackageDependency AutoRestCSharp =
            new PackageDependency(
                "Microsoft.Azure.AutoRest.CSharp",
                "3.0.0-beta.20210218.1");

        public static readonly PackageDependency AzureCore =
            new PackageDependency(
                "Azure.Core",
                new Version(1, 6, 0));
    }
}