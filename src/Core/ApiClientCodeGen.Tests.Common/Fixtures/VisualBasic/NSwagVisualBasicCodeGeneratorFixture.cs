using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.NSwag;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.VisualBasic
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagVisualBasicCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<INSwagOptions> OptionsMock = new Mock<INSwagOptions>();
        public string CSharpCode = null!;
        public string VisualBasicCode = null!;

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
                defaultNamespace,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                OptionsMock.Object);

            CSharpCode = codeGenerator.GenerateCode(ProgressReporterMock.Object);

            // Convert C# to VB.NET
            var converter = new CSharpToVisualBasicLanguageConverter();
            VisualBasicCode = converter.ConvertAsync(CSharpCode).GetAwaiter().GetResult();
        }
    }
}
