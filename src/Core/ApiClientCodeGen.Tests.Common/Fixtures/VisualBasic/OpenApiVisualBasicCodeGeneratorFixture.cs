using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Converters;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Core.External;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Tests.Common.Fixtures.VisualBasic
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class OpenApiVisualBasicCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string CSharpCode = null!;
        public string VisualBasicCode = null!;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();

            OptionsMock.Setup(c => c.JavaPath).Returns(PathProvider.GetInstalledJavaPath());

            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new DefaultOpenApiGeneratorOptions(),
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            CSharpCode = codeGenerator.GenerateCode(ProgressReporterMock.Object);

            // Convert C# to VB.NET
            var converter = new CSharpToVisualBasicLanguageConverter();
            VisualBasicCode = converter.ConvertAsync(CSharpCode).GetAwaiter().GetResult();
        }
    }
}
