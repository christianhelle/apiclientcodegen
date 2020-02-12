using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiCSharpCodeGeneratorTests
    {
        private readonly Mock<IGeneralOptions> optionsMock = new Mock<IGeneralOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();

        [TestInitialize]
        public void Init()
            => new OpenApiCSharpCodeGenerator(
                    "Swagger.json",
                    new Fixture().Create<string>(),
                    optionsMock.Object,
                    new ProcessLauncher())
                .GenerateCode(progressMock.Object);

        [TestMethod]
        public void Reads_SwaggerCodegenPath()
            => optionsMock.Verify(c => c.OpenApiGeneratorPath);

        [TestMethod]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
    }
}
