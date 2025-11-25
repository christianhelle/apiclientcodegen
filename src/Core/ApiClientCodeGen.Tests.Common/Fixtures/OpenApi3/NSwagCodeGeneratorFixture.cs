using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3
{
    public class NSwagCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        public readonly Mock<IProcessLauncher> ProcessLauncherMock = new Mock<IProcessLauncher>();
        public readonly Mock<IDependencyInstaller> DependencyInstallerMock = new Mock<IDependencyInstaller>();
        public string Code;

        protected override void OnInitialize()
        {
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            OptionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            OptionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var defaultNamespace = "GeneratedCode";
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                defaultNamespace,
                ProcessLauncherMock.Object,
                DependencyInstallerMock.Object,
                OptionsMock.Object);

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}