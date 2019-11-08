using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiDocumentExtensionsTests
    {
        [TestMethod]
        public async Task Can_GenerateClassName_From_DocumentTitle()
            => (await OpenApiDocument.FromJsonAsync(File.ReadAllText("Swagger.json")))
            .GenerateClassName(true)
            .Should()
            .Be("PetstoreClient");

        [TestMethod]
        public async Task Can_GenerateClassName_From_FileName()
            => (await OpenApiDocument.FromFileAsync("Swagger.json"))
                .GenerateClassName(false)
                .Should()
                .Be("Swagger");
    }
}
