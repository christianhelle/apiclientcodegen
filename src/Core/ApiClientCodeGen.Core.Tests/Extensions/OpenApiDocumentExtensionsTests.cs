using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Extensions;
using FluentAssertions;
using NSwag;

namespace ApiClientCodeGen.Core.Tests.Extensions
{
    public class OpenApiDocumentExtensionsTests : TestWithResources
    {
        private readonly string swaggerJson;

        public OpenApiDocumentExtensionsTests() 
            => swaggerJson = ReadAllText("Swagger.json");

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
        {
            var swaggerFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                SwaggerJsonFilename);
            (await OpenApiDocument.FromFileAsync(swaggerFilePath))
                .GenerateClassName(false)
                .Should()
                .Be(Path.GetFileNameWithoutExtension(swaggerFilePath));
        }
        
        [Xunit.Fact]
        public async Task Can_GenerateClassName_From_Json_Async()
            => (await OpenApiDocument.FromJsonAsync(swaggerJson))
                .GenerateClassName(false)
                .Should()
                .Be("PetstoreClient");
    }
}
