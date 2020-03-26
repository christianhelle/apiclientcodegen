using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;


namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    
    // [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiDocumentFactoryTests
    {
        [Xunit.Fact]
        public void Does_Not_Return_Null()
            => new OpenApiDocumentFactory()
                .GetDocument("Swagger.json")
                .Should()
                .NotBeNull();
    }
}