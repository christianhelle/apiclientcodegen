using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Resources;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class TestWithResources
    {
        public const string SwaggerJson = "Swagger.json";
        public const string SwaggerYaml = "Swagger.yaml";
        public const string SwaggerNswag = "Swagger.nswag";
        public const string SwaggerV3Json = "Swagger_v3.json";
        public const string SwaggerV3Yaml = "Swagger_v3.yaml";
        public const string SwaggerV3Nswag = "Swagger_v3.nswag";

        protected string SwaggerJsonFilename { get; } = $"Swagger{Guid.NewGuid():N}.json";
        protected string SwaggerYamlFilename { get; } = $"Swagger{Guid.NewGuid():N}.yaml";
        protected string SwaggerNSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.nswag";
        protected string SwaggerV3JsonFilename { get; } = $"Swagger{Guid.NewGuid():N}.json";
        protected string SwaggerV3YamlFilename { get; } = $"Swagger{Guid.NewGuid():N}.yaml";
        protected string SwaggerV3NSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.nswag";

        protected TestWithResources()
        {
            CreateFileFromEmbeddedResource(SwaggerJson, SwaggerJsonFilename);
            CreateFileFromEmbeddedResource(SwaggerYaml, SwaggerYamlFilename);
            CreateFileFromEmbeddedResource(SwaggerNswag, SwaggerNSwagFilename);
            CreateFileFromEmbeddedResource(SwaggerV3Json, SwaggerV3JsonFilename);
            CreateFileFromEmbeddedResource(SwaggerV3Yaml, SwaggerV3YamlFilename);
            CreateFileFromEmbeddedResource(SwaggerV3Nswag, SwaggerV3NSwagFilename);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Ssl3;
        }

        private static void CreateFileFromEmbeddedResource(string resourceName, string outputFile)
        {
            var directory = Directory.GetCurrentDirectory();
            using (var source = EmbeddedResources.GetStream(resourceName))
            using (var writer = File.Create(Path.Combine(directory, outputFile)))
                source.CopyTo(writer);
        }

        protected string ReadAllText(string resourceName)
        {
            using (var stream = EmbeddedResources.GetStream(resourceName))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}