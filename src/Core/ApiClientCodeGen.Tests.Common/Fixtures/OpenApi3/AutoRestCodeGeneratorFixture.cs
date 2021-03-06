using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3
{
    public class AutoRestCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IAutoRestOptions> OptionsMock = new Mock<IAutoRestOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            OptionsMock.Setup(c => c.AddCredentials).Returns(true);
            OptionsMock.Setup(c => c.UseDateTimeOffset).Returns(true);
            OptionsMock.Setup(c => c.UseInternalConstructors).Returns(true);

            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher(),
                new OpenApiDocumentFactory(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())));

            OptionsMock.Setup(c => c.OverrideClientName).Returns(true);
            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
            Code.Should().NotBeNullOrWhiteSpace();
        }
    }
}