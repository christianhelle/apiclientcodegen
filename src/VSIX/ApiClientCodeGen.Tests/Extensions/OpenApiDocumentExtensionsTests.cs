using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
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
        public async Task Can_GenerateClassName_From_Json_Using_DocumentTitle_Async()
        {
            (await OpenApiDocument.FromJsonAsync(swaggerJson))
                .GenerateClassName()
                .Should()
                .Be("PetstoreClient");
        }

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Without_DocumentTitle_Async()
        {
            var sut = await OpenApiDocument.FromJsonAsync(swaggerJson);
            sut.Info.Title = null;
            sut.GenerateClassName()
                .Should()
                .Be("ApiClient");
        }

        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_FileName_Async()
            => (await OpenApiDocument.FromFileAsync(SwaggerJsonFilename))
                .GenerateClassName(false)
                .Should()
                .Be(SwaggerJsonFilename.Replace(".json", string.Empty));
        
        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Async()
            => (await OpenApiDocument.FromJsonAsync(swaggerJson))
                .GenerateClassName(false)
                .Should()
                .Be("PetstoreClient");
    }
}
