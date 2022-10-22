using System.IO;
using System.Threading.Tasks;
using Rapicgen.Generators.NSwagStudio;
using Rapicgen.Options;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;

using Moq;

namespace Rapicgen.IntegrationTests.VisualBasic
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    // [DeploymentItem("Resources/Swagger.nswag")]
    // [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioVisualBasicCodeGeneratorTests
    {
        private Mock<IGeneralOptions> optionsMock;
        private IGeneralOptions options;

        [TestInitialize]
        public void Init()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;
        }

        [Xunit.Fact]
        public async Task NSwagStudio_Generate_Code_Using_NSwagStudio()
        {
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                File.ReadAllText(SwaggerJsonFilename),
                "https://petstore.swagger.io/v2/swagger.json");

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Petstore.nswag"), options)
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            var outputFile = Path.GetFullPath("PetstoreClient.cs");
            File.Exists(outputFile)
                .Should()
                .BeTrue();

            var csharp = File.ReadAllText(outputFile);
            var result = await CodeConverter.Convert(new CodeWithOptions(csharp));

            result.ConvertedCode
                .Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}
