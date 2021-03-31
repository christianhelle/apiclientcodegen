using ApiClientCodeGen.Tests.Common;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorYamlTests : TestWithResources
    {
        private readonly Mock<IGeneralOptions> optionsMock = new Mock<IGeneralOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IProcessLauncher> processMock = new Mock<IProcessLauncher>();
        private readonly Mock<IDependencyInstaller> dependencyMock = new Mock<IDependencyInstaller>();

        public OpenApiCSharpCodeGeneratorYamlTests()
            => new OpenApiCSharpCodeGenerator(
                    SwaggerYamlFilename,
                    new Fixture().Create<string>(),
                    optionsMock.Object,
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
