using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : TestWithResources
    {
        private Mock<IGeneralOptions> optionsMock;
        private IGeneralOptions options;

        public NSwagStudioCodeGeneratorTests()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;
        }

        [Xunit.Fact]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(Path.GetFullPath(SwaggerV3NSwagFilename), options, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();
        
        [Xunit.Fact]
        public async Task NSwagStudio_Generate_Code_Using_NSwagStudio_From_SwaggerSpec()
        {
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                new EnterOpenApiSpecDialogResult(File.ReadAllText(SwaggerV3NSwagFilename), "PetstoreClient", "https://petstore.swagger.io/v2/swagger.json"),
                new Mock<INSwagStudioOptions>().Object);

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Petstore.nswag"), options, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            File.Exists(Path.GetFullPath("PetstoreClient.cs"))
                .Should()
                .BeTrue();
        }

        [Xunit.Fact]
        public void Reads_NSwagPath_From_Options()
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerV3NSwagFilename), 
                    options,
                    new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object);

            optionsMock.Verify(c => c.NSwagPath);
        }

        [Xunit.Fact]
        public void GetNSwagPath_ForceDownload()
            => new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerV3NSwagFilename),
                    options,
                    new ProcessLauncher())
                .GetNSwagPath(true)
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
