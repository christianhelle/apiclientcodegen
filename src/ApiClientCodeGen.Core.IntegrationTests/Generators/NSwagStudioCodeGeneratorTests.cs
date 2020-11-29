using System;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{

    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : TestWithResources
    {
        private readonly Mock<IGeneralOptions> optionsMock;

        public NSwagStudioCodeGeneratorTests()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
        }

        [Xunit.Fact]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    optionsMock.Object,
                    new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

        [Xunit.Retry]
        public async Task NSwagStudio_Generate_Code_Using_NSwagStudio_From_SwaggerSpec()
        {
            var options = new Mock<INSwagStudioOptions>();
            options.Setup(c => c.UseDocumentTitle).Returns(false);
            options.Setup(c => c.GenerateDtoTypes).Returns(true);

            var outputFilename = $"PetstoreClient{Guid.NewGuid():N}";
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                new EnterOpenApiSpecDialogResult(
                    ReadAllText(SwaggerV3Json),
                    outputFilename,
                    "https://petstore.swagger.io/v2/swagger.json"),
                options.Object);

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, contents);

            new NSwagStudioCodeGenerator(tempFile, optionsMock.Object, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();
        }

        [Xunit.Fact]
        public void Reads_NSwagPath_From_Options()
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    optionsMock.Object,
                    new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object);

            optionsMock.Verify(c => c.NSwagPath);
        }

        [Xunit.Fact]
        public void GetNSwagPath_Returns_NotNullOrWhiteSpace()
        {
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    optionsMock.Object,
                    new ProcessLauncher())
                .GetNSwagPath()
                .Should()
                .NotBeNullOrWhiteSpace();
        }

        [Xunit.Fact]
        public void GetNSwagPath_ForceDownload()
            => new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename),
                    optionsMock.Object,
                    new ProcessLauncher(),
                    true)
                .GetNSwagPath()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
