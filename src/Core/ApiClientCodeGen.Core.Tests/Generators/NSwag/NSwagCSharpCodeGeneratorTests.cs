using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.NSwag
{
    public class NSwagCSharpCodeGeneratorTests : TestWithResources
    {
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IProcessLauncher> processLauncherMock = new Mock<IProcessLauncher>();
        private readonly Mock<IDependencyInstaller> dependencyInstallerMock = new Mock<IDependencyInstaller>();
        private readonly Mock<INSwagOptions> optionsMock = new Mock<INSwagOptions>();
        private string code;

        protected override void OnInitialize()
        {
            optionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);
            optionsMock.Setup(c => c.UseDocumentTitle).Returns(true);
            optionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            optionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            optionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            optionsMock.Setup(c => c.UseBaseUrl).Returns(false);
            optionsMock.Setup(c => c.ParameterDateTimeFormat).Returns("s");

            var sut = new NSwagCSharpCodeGenerator(
                SwaggerJsonFilename,
                "GeneratedCode",
                processLauncherMock.Object,
                dependencyInstallerMock.Object,
                optionsMock.Object);
            
            code = sut.GenerateCode(progressMock.Object);
        }

        [Fact]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.AtLeast(3));

        [Fact]
        public void Installs_NSwag_Dependency()
            => dependencyInstallerMock.Verify(
                c => c.InstallNSwag(),
                Times.Once);

        [Fact]
        public void Launches_NSwag_Process()
            => processLauncherMock.Verify(
                c => c.Start(
                    It.Is<string>(s => s == "nswag"),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);

        [Fact]
        public void Generated_Code()
            => code.Should().NotBeNull();
    }
}
