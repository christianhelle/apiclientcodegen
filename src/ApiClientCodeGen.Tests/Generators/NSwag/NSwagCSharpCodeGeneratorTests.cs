using System.Threading.Tasks;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCSharpCodeGeneratorTests
    {
        private readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private readonly Mock<IVsGeneratorProgress> progressMock = new Mock<IVsGeneratorProgress>();
        private readonly Mock<IOpenApiDocumentFactory> documentFactoryMock = new Mock<IOpenApiDocumentFactory>();
        private readonly Mock<INSwagCodeGeneratorSettingsFactory> settingsMock = new Mock<INSwagCodeGeneratorSettingsFactory>();
        private OpenApiDocument document;
        private string code;

        [TestInitialize]
        public async Task Init()
        {
            document = await OpenApiDocument.FromFileAsync("Swagger.json");
            documentFactoryMock.Setup(c => c.GetDocument("Swagger.json"))
                .Returns(document);

            settingsMock.Setup(c => c.GetGeneratorSettings(It.IsAny<OpenApiDocument>()))
                .Returns(new CSharpClientGeneratorSettings());

            var sut = new NSwagCSharpCodeGenerator(
                "Swagger.json",
                Test.CreateAnnonymous<string>(),
                optionsMock.Object,
                documentFactoryMock.Object,
                settingsMock.Object);
            
            code = sut.GenerateCode(progressMock.Object);
        }

        [TestMethod]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(4));

        [TestMethod]
        public void Gets_Document_From_Factory()
            => documentFactoryMock.Verify(
                c => c.GetDocument("Swagger.json"),
                Times.Once);

        [TestMethod]
        public void Gets_GeneratorSettings()
            => settingsMock.Verify(
                c => c.GetGeneratorSettings(document),
                Times.Once);

        [TestMethod]
        public void Generated_Code()
            => code.Should().NotBeNullOrWhiteSpace();
    }
}
