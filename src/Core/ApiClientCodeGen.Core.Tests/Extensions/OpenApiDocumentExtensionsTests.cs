using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Models;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.Extensions
{
    public class OpenApiDocumentExtensionsTests : TestWithResources
    {
        [Xunit.Fact]
        public void Can_GenerateClassName_From_Json_Using_DocumentTitle()
        {
            var doc = new SimpleOpenApiDocument { Info = new SimpleOpenApiInfo { Title = "Swagger Petstore" } };
            doc.GenerateClassName()
                .Should()
                .Be("PetstoreClient");
        }

        [Xunit.Fact]
        public void Can_GenerateClassName_From_Json_Without_DocumentTitle()
        {
            var doc = new SimpleOpenApiDocument { Info = new SimpleOpenApiInfo { Title = null }, DocumentPath = "Swagger.json" };
            doc.GenerateClassName()
                .Should()
                .Be("ApiClient");
        }

        [Xunit.Fact]
        public void Can_GenerateClassName_From_FileName()
        {
            var doc = new SimpleOpenApiDocument { Info = new SimpleOpenApiInfo { Title = "PetstoreClient" }, DocumentPath = "Swagger.json" };
            doc.GenerateClassName(false)
                .Should()
                .Be("Swagger");
        }
    }
}
