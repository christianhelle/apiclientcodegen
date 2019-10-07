using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCodeGeneratorSettingsFactoryTests
    {
        [TestMethod]
        public void Does_Not_Return_Null()
            => new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    Test.CreateDummy<INSwagOptions>())
                .GetGeneratorSettings(
                    new OpenApiDocumentFactory()
                        .GetDocument("Swagger.json"))
                .Should()
                .NotBeNull();
    }
}