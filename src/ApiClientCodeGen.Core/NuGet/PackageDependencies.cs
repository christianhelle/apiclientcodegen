using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet
{
    public static class PackageDependencies
    {
        public static readonly PackageDependency NewtonsoftJson =
            new PackageDependency(
                "Newtonsoft.Json",
                new Version(12, 0, 3, 0),
                false);

        public static readonly PackageDependency MicrosoftRestClientRuntime =
            new PackageDependency(
                "Microsoft.Rest.ClientRuntime",
                new Version(2, 3, 21, 0));

        public static readonly PackageDependency RestSharp =
            new PackageDependency(
                "RestSharp",
                new Version(105, 1, 0, 0));

        public static readonly PackageDependency JsonSubTypes =
            new PackageDependency(
                "JsonSubTypes",
                new Version(1, 2, 0, 0));

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
    }
}