using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiDocumentFactoryTests
    {
        [TestMethod, Xunit.Fact]
        public void Does_Not_Return_Null()
            => new OpenApiDocumentFactory()
                .GetDocument("Swagger.json")
                .Should()
                .NotBeNull();
    }
}