using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorTests : TestWithResources
    {
        private Mock<IGeneralOptions> optionsMock;
        private Mock<IProcessLauncher> processMock;
        private Mock<IProgressReporter> progressMock;
        private IGeneralOptions options;

        public NSwagStudioCodeGeneratorTests()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(Path.GetTempFileName());
            options = optionsMock.Object;

            processMock = new Mock<IProcessLauncher>();
            progressMock = new Mock<IProgressReporter>();
        }

        [Xunit.Fact]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(Path.GetFullPath("Swagger.nswag"), options, processMock.Object)
                .GenerateCode(progressMock.Object)
                .Should()
                .BeNull();

        [Xunit.Fact]
        public void Reads_NSwagPath_From_Options()
        {
            progressMock = new Mock<IProgressReporter>();
            new NSwagStudioCodeGenerator(
                    Path.GetFullPath("Swagger.nswag"), 
                    options,
                    processMock.Object)
                .GenerateCode(progressMock.Object);

            optionsMock.Verify(c => c.NSwagPath);
        }
    }
}
