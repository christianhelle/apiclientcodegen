using System.IO;
using System.Threading.Tasks;
using Moq;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class KiotaCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();

        public string Code { get; private set; }

        protected override async Task OnInitializeAsync()
        {
            const string defaultNamespace = "GeneratedCode";
            var codeGenerator = new KiotaCodeGenerator(
                Path.GetFullPath(SwaggerV3YamlFilename),
                defaultNamespace,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}