using System.IO;
using System.Threading.Tasks;
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
            optionsMock.Setup(c => c.ClassStyle).Returns("Poco");
            
            var sut = new NSwagCSharpCodeGenerator(
                SwaggerJsonFilename,
                "GeneratedCode",
                optionsMock.Object,
                processLauncherMock.Object,
                dependencyInstallerMock.Object);
            
            code = sut.GenerateCode(progressMock.Object);
        }

        [Fact]
        public void Installs_NSwag()
            => dependencyInstallerMock.Verify(c => c.InstallNSwag(), Times.Once);

        [Fact]
        public void Starts_Process()
            => processLauncherMock.Verify(
                c => c.Start(
                    "nswag", 
                    It.IsAny<string>(), 
                    It.IsAny<string>()), 
                Times.Once);
    }
}
