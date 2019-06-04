using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiCodeGeneratorTests
    {
        private readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private string code = null;

        [TestInitialize]
        public void Init()
        {
            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                GetType().Namespace);

            code = codeGenerator.GenerateCode(mock.Object);
        }

        [TestCleanup]
        public void CleanUp()
            => DependencyUninstaller.UninstallOpenApiGenerator();

        [TestMethod]
        public void Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);
    }
}
