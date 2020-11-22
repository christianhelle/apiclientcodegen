using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    public class NSwagCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        public readonly string Code = null;

        public NSwagCodeGeneratorFixture()
        {
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            OptionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            OptionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var defaultNamespace = typeof(NSwagCodeGeneratorTests).Namespace;
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                new OpenApiDocumentFactory(),
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, OptionsMock.Object));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}