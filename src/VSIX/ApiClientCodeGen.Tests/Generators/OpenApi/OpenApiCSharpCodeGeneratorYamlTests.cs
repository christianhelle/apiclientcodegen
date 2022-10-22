using ApiClientCodeGen.Tests.Common;
using AutoFixture;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Moq;

namespace Rapicgen.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorYamlTests : TestWithResources
    {
        private readonly Mock<IGeneralOptions> optionsMock = new Mock<IGeneralOptions>();
        private readonly Mock<IOpenApiGeneratorOptions> openApiGeneratorOptionsMock = new Mock<IOpenApiGeneratorOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IProcessLauncher> processMock = new Mock<IProcessLauncher>();
        private readonly Mock<IDependencyInstaller> dependencyMock = new Mock<IDependencyInstaller>();

        public OpenApiCSharpCodeGeneratorYamlTests()
            => new OpenApiCSharpCodeGenerator(
                    SwaggerYamlFilename,
                    new Fixture().Create<string>(),
                    optionsMock.Object,
                    openApiGeneratorOptionsMock.Object,
                    processMock.Object,
                    dependencyMock.Object)
                .GenerateCode(progressMock.Object);

        [Xunit.Fact]
        public void Reads_SwaggerCodegenPath()
            => optionsMock.Verify(c => c.OpenApiGeneratorPath);

        [Xunit.Fact]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
    }
}
