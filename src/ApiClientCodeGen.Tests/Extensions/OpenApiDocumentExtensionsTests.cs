using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    public class OpenApiDocumentExtensionsTests : TestWithResources
    {
        private readonly string swaggerJson;

        public OpenApiDocumentExtensionsTests() 
            => swaggerJson = ReadAllText(SwaggerJson);

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Using_DocumentTitle()
        {
            (await OpenApiDocument.FromJsonAsync(swaggerJson))
                .GenerateClassName()
                .Should()
                .Be("PetstoreClient");
        }

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Without_DocumentTitle()
        {
            var sut = await OpenApiDocument.FromJsonAsync(swaggerJson);
            sut.Info.Title = null;
            sut.GenerateClassName()
                .Should()
                .Be("ApiClient");
        }

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_FileName()
            => (await OpenApiDocument.FromFileAsync(SwaggerJsonFilename))
                .GenerateClassName(false)
                .Should()
                .Be(SwaggerJsonFilename);
        
        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json()
            => (await OpenApiDocument.FromJsonAsync(swaggerJson))
                .GenerateClassName(false)
                .Should()
                .Be("PetstoreClient");
    }
}
