using System.Diagnostics.CodeAnalysis;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Resources;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class TestWithResources
    {
        private static readonly object SyncLock = new object();

        protected TestWithResources()
        {
            lock (SyncLock)
            {
                CreateFileFromEmbeddedResource("Swagger.json");
                CreateFileFromEmbeddedResource("Swagger.nswag");
            }
        }

        private static void CreateFileFromEmbeddedResource(string resourceName)
        {
            var directory = Directory.GetCurrentDirectory();
            using var source = EmbeddedResources.GetStream(resourceName);
            using var writer = File.Create(Path.Combine(directory, resourceName));
            source.CopyTo(writer);
        }

        protected string ReadAllText(string resourceName)
        {
            using var stream = EmbeddedResources.GetStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}