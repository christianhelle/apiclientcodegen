using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.Yaml
{
    public class OpenApiCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public Mock<IGeneralOptions> OptionsMock;
        public readonly string Code = null;

        public OpenApiCodeGeneratorFixture()
        {
            OptionsMock = new Mock<IGeneralOptions>();
            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerYamlFilename),
                typeof(OpenApiCodeGeneratorYamlTests).Namespace,
                OptionsMock.Object,
                new ProcessLauncher());

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}