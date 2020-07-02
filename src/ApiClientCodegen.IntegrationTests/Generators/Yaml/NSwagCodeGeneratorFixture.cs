using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.Yaml
{
    public class NSwagCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        private readonly NSwagCSharpCodeGenerator codeGenerator;

        public NSwagCodeGeneratorFixture()
        {
            var defaultNamespace = typeof(NSwagCodeGeneratorTests).Namespace;
            codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath("Swagger.yaml"),
                new OpenApiDocumentFactory(),
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, OptionsMock.Object));
        }

        public void GenerateCode()
        {
            codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}