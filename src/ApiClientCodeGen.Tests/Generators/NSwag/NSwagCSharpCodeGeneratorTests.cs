using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using FluentAssertions;
using Moq;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{
    public class NSwagCSharpCodeGeneratorTests : TestWithResources
    {
        private readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IOpenApiDocumentFactory> documentFactoryMock = new Mock<IOpenApiDocumentFactory>();
        private readonly Mock<INSwagCodeGeneratorSettingsFactory> settingsMock = new Mock<INSwagCodeGeneratorSettingsFactory>();
        private OpenApiDocument document;
        private string code;

        public NSwagCSharpCodeGeneratorTests()
        {
            document = OpenApiDocument.FromFileAsync(SwaggerJsonFilename).GetAwaiter().GetResult();
            documentFactoryMock.Setup(c => c.GetDocument(SwaggerJsonFilename))
                .Returns(document);

            settingsMock.Setup(c => c.GetGeneratorSettings(It.IsAny<OpenApiDocument>()))
                .Returns(new CSharpClientGeneratorSettings());

            var sut = new NSwagCSharpCodeGenerator(
                SwaggerJsonFilename,
                documentFactoryMock.Object,
                settingsMock.Object);
            
            code = sut.GenerateCode(progressMock.Object);
        }

        [Fact]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(4));

        [Fact]
        public void Gets_Document_From_Factory()
            => documentFactoryMock.Verify(
                c => c.GetDocument(SwaggerJsonFilename),
                Times.Once);

        [Fact]
        public void Gets_GeneratorSettings()
            => settingsMock.Verify(
                c => c.GetGeneratorSettings(document),
                Times.Once);

        [Fact]
        public void Generated_Code()
            => code.Should().NotBeNullOrWhiteSpace();
    }
}
