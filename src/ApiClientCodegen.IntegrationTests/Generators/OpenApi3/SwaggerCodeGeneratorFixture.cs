using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.Swagger;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    public class SwaggerCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock= new Mock<IGeneralOptions>();
        public readonly string Code = null;

        public SwaggerCodeGeneratorFixture()
        {
            OptionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new SwaggerCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                typeof(SwaggerCodeGeneratorTests).Namespace,
                OptionsMock.Object,
                new ProcessLauncher());

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}