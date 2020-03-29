using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    public class OpenApiDocumentFactoryTests : TestWithResources
    {
        [Fact]
        public void Does_Not_Return_Null()
            => new OpenApiDocumentFactory()
                .GetDocument("Swagger.json")
                .Should()
                .NotBeNull();
    }
}