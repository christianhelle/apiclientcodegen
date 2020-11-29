using System.IO;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    public class OpenApiCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public Mock<IGeneralOptions> OptionsMock;
        public string Code;

        public OpenApiCodeGeneratorFixture()
        {
            OptionsMock = new Mock<IGeneralOptions>();
            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetJavaPath());
        }

        protected override void OnInitialize()
        {
            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher());

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}