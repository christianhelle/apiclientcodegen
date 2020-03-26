using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.VisualBasic
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class SwaggerVisualBasicCodeGeneratorTests
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static Mock<IGeneralOptions> optionsMock;
        private static string code = null;

        // [ClassInitialize]
        public static void Init(/* TestContext testContext */)
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new SwaggerCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(SwaggerCodeGeneratorTests).Namespace,
                optionsMock.Object);

            code = codeGenerator.GenerateCode(mock.Object);
        }

        // [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallSwaggerCodegen();

        [Xunit.Fact]
        public void Swagger_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void Swagger_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_JavaPath_From_Options() 
            => optionsMock.Verify(c => c.JavaPath);
    }
}
