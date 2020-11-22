using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.VisualBasic
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiVisualBasicCodeGeneratorTests
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static Mock<IGeneralOptions> optionsMock;
        private static string code = null;

        // [ClassInitialize]
        public static async Task InitAsync(/* TestContext testContext */)
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                typeof(OpenApiVisualBasicCodeGeneratorTests).Namespace,
                optionsMock.Object);

            var options = new CodeWithOptions(codeGenerator.GenerateCode(mock.Object));
            var result = await CodeConverter.Convert(options);

            code = result.ConvertedCode;
        }

        // [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallOpenApiGenerator();

        [Xunit.Fact]
        public void OpenApi_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void OpenApi_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_JavaPath_From_Options() 
            => optionsMock.Verify(c => c.JavaPath);
    }
}
