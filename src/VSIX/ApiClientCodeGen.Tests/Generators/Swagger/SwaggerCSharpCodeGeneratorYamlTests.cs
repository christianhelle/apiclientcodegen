using ApiClientCodeGen.Tests.Common;
using AutoFixture;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Moq;
using Xunit;

namespace Rapicgen.Tests.Generators.Swagger
{
    public class SwaggerCSharpCodeGeneratorYamlTests : TestWithResources
    {
        private readonly Mock<IGeneralOptions> optionsMock = new Mock<IGeneralOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IProcessLauncher> processMock = new Mock<IProcessLauncher>();
        private readonly Mock<IDependencyInstaller> dependencyMock = new Mock<IDependencyInstaller>();

        public SwaggerCSharpCodeGeneratorYamlTests()
            => new SwaggerCSharpCodeGenerator(
                    SwaggerYamlFilename,
                    new Fixture().Create<string>(),
                    optionsMock.Object,
                    processMock.Object,
                    dependencyMock.Object)
                .GenerateCode(progressMock.Object);

        [Fact]
        public void Reads_SwaggerCodegenPath()
            => optionsMock.Verify(c => c.SwaggerCodegenPath);

        [Fact]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
    }
}