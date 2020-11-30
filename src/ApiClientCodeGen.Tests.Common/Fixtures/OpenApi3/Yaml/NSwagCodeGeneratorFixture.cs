using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml
{
    public class NSwagCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        public string Code;

        public NSwagCodeGeneratorFixture()
        {
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            OptionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            OptionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);
        }

        protected override void  OnInitialize()
        {
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3YamlFilename),
                new OpenApiDocumentFactory(),
                new NSwagCodeGeneratorSettingsFactory("GeneratedCode", OptionsMock.Object));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}