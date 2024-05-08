using System.IO;
using System.Threading.Tasks;
using Moq;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Kiota;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class KiotaCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();

        public string Code { get; private set; }

        protected override Task OnInitializeAsync()
        {
            const string defaultNamespace = "GeneratedCode";
            var codeGenerator = new KiotaCodeGenerator(
                Path.GetFullPath(SwaggerV3JsonFilename),
                defaultNamespace,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                new DefaultKiotaOptions());

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
            return Task.CompletedTask;
        }
    }
}