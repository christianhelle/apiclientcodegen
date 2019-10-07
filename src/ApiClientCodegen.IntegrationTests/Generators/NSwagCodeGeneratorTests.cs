using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagCodeGeneratorTests
    {
        private static readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private static readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private static string code = null;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            optionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            optionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var defaultNamespace = typeof(NSwagCodeGeneratorTests).Namespace;
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                defaultNamespace,
                optionsMock.Object,
                new OpenApiDocumentFactory(), 
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, optionsMock.Object));

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

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetCoreApp() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, code, SupportedCodeGenerator.NSwag);

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, code, SupportedCodeGenerator.NSwag);
    }
}
