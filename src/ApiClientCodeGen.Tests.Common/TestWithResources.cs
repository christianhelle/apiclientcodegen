using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common.Resources;
using Polly;
using Xunit;

namespace ApiClientCodeGen.Tests.Common
{
    [ExcludeFromCodeCoverage]
    public abstract class TestWithResources : IAsyncLifetime
    {
        private static readonly object syncLock = new object();

        public const string SwaggerJson = "Swagger.json";
        public const string SwaggerYaml = "Swagger.yaml";
        public const string SwaggerNswag = "Swagger.nswag";
        public const string SwaggerLegacyNswag = "Swagger.legacy.nswag";
        public const string SwaggerV3Json = "Swagger_v3.json";
        public const string SwaggerV3Yaml = "Swagger_v3.yaml";
        public const string SwaggerV3Nswag = "Swagger_v3.nswag";
        public const string SwaggerV3LegacyNswag = "Swagger_v3.legacy.nswag";

        protected string SwaggerJsonFilename { get; } = $"Swagger{Guid.NewGuid():N}.json";
        protected string SwaggerYamlFilename { get; } = $"Swagger{Guid.NewGuid():N}.yaml";
        protected string SwaggerNSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.nswag";
        protected string SwaggerLegacyNSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.legacy.nswag";
        protected string SwaggerV3JsonFilename { get; } = $"Swagger{Guid.NewGuid():N}.json";
        protected string SwaggerV3YamlFilename { get; } = $"Swagger{Guid.NewGuid():N}.yaml";
        protected string SwaggerV3NSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.nswag";
        protected string SwaggerV3LegacyNSwagFilename { get; } = $"Swagger{Guid.NewGuid():N}.legacy.nswag";

        protected TestWithResources()
        {
            lock (syncLock)
            {
                CreateFileFromEmbeddedResource(SwaggerJson);
                CreateFileFromEmbeddedResource(SwaggerYaml);
                CreateFileFromEmbeddedResource(SwaggerNswag);
                CreateFileFromEmbeddedResource(SwaggerLegacyNswag);
                CreateFileFromEmbeddedResource(SwaggerV3Json);
                CreateFileFromEmbeddedResource(SwaggerV3Yaml);
                CreateFileFromEmbeddedResource(SwaggerV3Nswag);
                CreateFileFromEmbeddedResource(SwaggerV3LegacyNswag);

                CreateFileFromEmbeddedResource(SwaggerJson, SwaggerJsonFilename);
                CreateFileFromEmbeddedResource(SwaggerYaml, SwaggerYamlFilename);
                CreateFileFromEmbeddedResource(SwaggerNswag, SwaggerNSwagFilename);
                CreateFileFromEmbeddedResource(SwaggerLegacyNswag, SwaggerLegacyNSwagFilename);
                CreateFileFromEmbeddedResource(SwaggerV3Json, SwaggerV3JsonFilename);
                CreateFileFromEmbeddedResource(SwaggerV3Yaml, SwaggerV3YamlFilename);
                CreateFileFromEmbeddedResource(SwaggerV3Nswag, SwaggerV3NSwagFilename);
                CreateFileFromEmbeddedResource(SwaggerV3LegacyNswag, SwaggerV3LegacyNSwagFilename);
            }

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12 |
                                                       SecurityProtocolType.Ssl3;
            }
            catch (NotSupportedException)
            {
                // Ignore
            }
        }

        private static void CreateFileFromEmbeddedResource(string resourceName, string outputFile = null)
        {
            var directory = Directory.GetCurrentDirectory();
            using (var source = EmbeddedResources.GetStream(resourceName))
            using (var writer = File.Create(Path.Combine(directory, outputFile ?? resourceName)))
                source.CopyTo(writer);
        }

        protected string ReadAllText(string resourceName)
        {
            using (var stream = EmbeddedResources.GetStream(resourceName))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public async Task InitializeAsync()
        {
            TimeSpan SleepDurationProvider(int retryAttempt)
            {
                var duration = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                Trace.WriteLine($"Operation failed! Retrying in {duration}");
                return duration;
            }
            
            Policy
                .Handle<Exception>()
                .WaitAndRetry(3, SleepDurationProvider)
                .Execute(OnInitialize);
            
            await Policy
                .Handle<Exception>()
                .WaitAndRetry(3, SleepDurationProvider)
                .Execute(OnInitializeAsync);
        }

        protected void ThrowNotSupportedOnUnix()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                throw new NotSupportedException();
        }

        protected virtual void OnInitialize() { }

        protected virtual Task OnInitializeAsync() => Task.CompletedTask;

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}