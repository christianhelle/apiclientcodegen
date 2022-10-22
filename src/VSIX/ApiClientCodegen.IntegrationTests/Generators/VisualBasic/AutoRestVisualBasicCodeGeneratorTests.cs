using System.IO;
using Rapicgen.Generators.AutoRest;
using Rapicgen.IntegrationTests.Utility;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;

namespace Rapicgen.IntegrationTests.VisualBasic
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class AutoRestVisualBasicCodeGeneratorTests
    {
        private static readonly Mock<IProgressReporter> mock = new Mock<IProgressReporter>();
        private static string code = null;

        // [ClassInitialize]
        public static void Init(/* TestContext testContext */)
        {
            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                typeof(AutoRestVisualBasicCodeGeneratorTests).Namespace);

            var options = new CodeWithOptions(codeGenerator.GenerateCode(mock.Object));
            var result = CodeConverter
                .Convert(options)
                .GetAwaiter()
                .GetResult();

            code = result.ConvertedCode;
        }

        [Xunit.Fact]
        public void AutoRest_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void AutoRest_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);
    }
}
