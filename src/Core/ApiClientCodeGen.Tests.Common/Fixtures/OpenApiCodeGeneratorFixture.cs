using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures
{
    public class OpenApiCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock= new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();

            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());
            
            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
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