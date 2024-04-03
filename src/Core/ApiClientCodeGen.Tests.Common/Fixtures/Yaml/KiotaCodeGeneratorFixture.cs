using System.IO;
using System.Threading.Tasks;
using Moq;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.Yaml
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
                Path.GetFullPath(SwaggerYamlFilename),
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