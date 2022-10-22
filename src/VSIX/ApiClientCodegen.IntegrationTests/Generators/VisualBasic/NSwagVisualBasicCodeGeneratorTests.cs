using System.IO;
using System.Threading.Tasks;
using Rapicgen.Generators.NSwag;
using Rapicgen.Options;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;

namespace Rapicgen.IntegrationTests.VisualBasic
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class NSwagVisualBasicCodeGeneratorTests
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private static readonly Mock<IOpenApiDocumentFactory> documentFactoryMock = new Mock<IOpenApiDocumentFactory>();
        private static readonly Mock<INSwagCodeGeneratorSettingsFactory> settingsMock = new Mock<INSwagCodeGeneratorSettingsFactory>();
        private static string code = null;

        // [ClassInitialize]
        public static async Task InitAsync(/* TestContext testContext */)
        {
            var defaultNamespace = typeof(NSwagVisualBasicCodeGeneratorTests).Namespace;
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                defaultNamespace,
                optionsMock.Object,
                new OpenApiDocumentFactory(), 
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, optionsMock.Object));

            var options = new CodeWithOptions(codeGenerator.GenerateCode(mock.Object));
            var result = await CodeConverter.Convert(options);

            code = result.ConvertedCode;
        }

        [Xunit.Fact]
        public void NSwag_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NSwag_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => optionsMock.Verify(c => c.InjectHttpClient);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => optionsMock.Verify(c => c.GenerateClientInterfaces);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => optionsMock.Verify(c => c.GenerateDtoTypes);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => optionsMock.Verify(c => c.UseBaseUrl);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => optionsMock.Verify(c => c.ClassStyle);
    }
}
