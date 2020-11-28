using System.IO;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    public class AutoRestCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IAutoRestOptions> OptionsMock = new Mock<IAutoRestOptions>();
        public readonly string Code;

        public AutoRestCodeGeneratorFixture()
        {
            OptionsMock.Setup(c => c.AddCredentials).Returns(true);
            OptionsMock.Setup(c => c.UseDateTimeOffset).Returns(true);
            OptionsMock.Setup(c => c.UseInternalConstructors).Returns(true);

            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher(),
                new OpenApiDocumentFactory());

            OptionsMock.Setup(c => c.OverrideClientName).Returns(true);
            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}