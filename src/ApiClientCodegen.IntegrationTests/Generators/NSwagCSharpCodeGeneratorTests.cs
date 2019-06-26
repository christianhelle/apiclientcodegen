using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCSharpCodeGeneratorTests
    {
        private static readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private static readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private static string code = null;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(NSwagCSharpCodeGeneratorTests).Namespace,
                optionsMock.Object);

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

        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => optionsMock.Verify(c => c.InjectHttpClient);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => optionsMock.Verify(c => c.GenerateClientInterfaces);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => optionsMock.Verify(c => c.GenerateDtoTypes);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => optionsMock.Verify(c => c.UseBaseUrl);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => optionsMock.Verify(c => c.ClassStyle);
    }
}
