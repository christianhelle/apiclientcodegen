using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class SwaggerCodeGeneratorTests
    {
        private readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private string code = null;

        [TestInitialize]
        public void Init()
        {
            var codeGenerator = new SwaggerCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                GetType().Namespace);

            code = codeGenerator.GenerateCode(mock.Object);
        }

        [TestCleanup]
        public void CleanUp()
            => DependencyUninstaller.UninstallSwaggerCodegen();

        [TestMethod]
        public void Swagger_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void Swagger_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);
    }
}
