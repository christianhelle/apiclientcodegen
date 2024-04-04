using System.IO;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Moq;
using Rapicgen.Core.External;

namespace ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3.Yaml
{
    public class OpenApiCodeGeneratorFixture : TestWithResources
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IGeneralOptions> OptionsMock = new Mock<IGeneralOptions>();
        public string Code;

        protected override void OnInitialize()
        {
            ThrowNotSupportedOnUnix();
            
            OptionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());
            
            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath(SwaggerV3YamlFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new DefaultOpenApiGeneratorOptions(),
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }
    }
}