using System.IO;
using ApiClientCodeGen.Tests.Common;
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
        private readonly string code;
        private readonly string path;

        public NSwagStudioCodeGeneratorTests()
        {
            path = Path.GetTempFileName();
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(path);
            options = optionsMock.Object;

            processMock = new Mock<IProcessLauncher>();
            progressMock = new Mock<IProgressReporter>();
            
            code = new NSwagStudioCodeGenerator(
                    Path.GetFullPath(SwaggerNSwagFilename), 
                    options,
                    processMock.Object)
                .GenerateCode(progressMock.Object);
        }

        [Xunit.Fact]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => code.Should().BeNull();

        [Xunit.Fact]
        public void Reads_NSwagPath_From_Options() 
            => optionsMock.Verify(c => c.NSwagPath);

        [Xunit.Fact]
        public void Reports_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()));

        [Xunit.Fact]
        public void Launches_NSwag()
            => processMock.Verify(
                c => c.Start(
                    path,
                    It.IsAny<string>(),
                    It.IsAny<string>()));
    }
}
