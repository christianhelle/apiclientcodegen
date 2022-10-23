using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace ApiClientCodeGen.Tests.Common.Fixtures
{
    public class NSwagCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.InjectHttpClient).Returns(true);
            OptionsMock.Setup(c => c.GenerateClientInterfaces).Returns(true);
            OptionsMock.Setup(c => c.GenerateDtoTypes).Returns(true);
            OptionsMock.Setup(c => c.UseBaseUrl).Returns(true);
            OptionsMock.Setup(c => c.ClassStyle).Returns(CSharpClassStyle.Poco);

            var defaultNamespace = "GeneratedCode";
            var codeGenerator = new NSwagCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                new OpenApiDocumentFactory(),
                new NSwagCodeGeneratorSettingsFactory(defaultNamespace, OptionsMock.Object));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}