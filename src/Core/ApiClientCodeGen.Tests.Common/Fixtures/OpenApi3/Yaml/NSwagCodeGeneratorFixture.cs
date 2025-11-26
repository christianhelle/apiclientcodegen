using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
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
                "GeneratedCode",
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                OptionsMock.Object);

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}