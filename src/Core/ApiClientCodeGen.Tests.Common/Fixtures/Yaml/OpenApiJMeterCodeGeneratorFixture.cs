using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.Yaml
{
    public class OpenApiJMeterCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();
            
            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiJMeterCodeGenerator(
                Path.GetFullPath(SwaggerYamlFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}