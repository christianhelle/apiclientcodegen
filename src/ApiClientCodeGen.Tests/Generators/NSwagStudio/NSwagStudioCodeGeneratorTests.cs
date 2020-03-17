using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioCodeGeneratorTests
    {
        private Mock<IGeneralOptions> optionsMock;
        private Mock<IProcessLauncher> processMock;
        private Mock<IProgressReporter> progressMock;
        private IGeneralOptions options;

        [TestInitialize]
        public void Init()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(Path.GetTempFileName());
            options = optionsMock.Object;

            processMock = new Mock<IProcessLauncher>();
            progressMock = new Mock<IProgressReporter>();
        }

        [TestMethod, Xunit.Fact]
        public void NSwagStudio_Generate_Code_Using_NSwagStudio()
            => new NSwagStudioCodeGenerator(Path.GetFullPath("Swagger.nswag"), options, processMock.Object)
                .GenerateCode(progressMock.Object)
                .Should()
                .BeNull();

        [TestMethod, Xunit.Fact]
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
