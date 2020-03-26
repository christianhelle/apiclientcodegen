using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;

using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    
    // [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiDocumentExtensionsTests
    {
        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Using_DocumentTitle()
            => (await OpenApiDocument.FromJsonAsync(File.ReadAllText("Swagger.json")))
            .GenerateClassName()
            .Should()
            .Be("PetstoreClient");

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_FileName()
            => (await OpenApiDocument.FromFileAsync("Swagger.json"))
                .GenerateClassName(false)
                .Should()
                .Be("Swagger");
        
        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json()
            => (await OpenApiDocument.FromJsonAsync(File.ReadAllText("Swagger.json")))
                .GenerateClassName(false)
                .Should()
                .Be("PetstoreClient");
    }
}
