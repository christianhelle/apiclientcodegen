using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCodeGeneratorTests
    {
        private readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private string code = null;

        [TestInitialize]
        public void Init()
        {
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                GetType().Namespace);

            code = codeGenerator.GenerateCode(mock.Object);
        }

        [TestMethod]
        public void NSwag_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void NSwag_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);
    }
}
