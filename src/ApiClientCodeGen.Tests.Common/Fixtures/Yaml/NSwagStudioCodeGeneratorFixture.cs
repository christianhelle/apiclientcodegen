using System;
using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using FluentAssertions;
using Moq;

namespace ApiClientCodeGen.Tests.Common.Fixtures.Yaml
{
    public class NSwagStudioCodeGeneratorFixture : TestWithResources
    {
        public string Code { get; private set; }

        protected override async Task OnInitializeAsync()
        {
            var generalOptions = new Mock<IGeneralOptions>();
            generalOptions.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());

            var options = new Mock<INSwagStudioOptions>();
            options.Setup(c => c.UseDocumentTitle).Returns(false);
            options.Setup(c => c.GenerateDtoTypes).Returns(true);

            var outputFilename = $"PetstoreClient{Guid.NewGuid():N}";
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                new EnterOpenApiSpecDialogResult(
                    ReadAllText(SwaggerYaml),
                    outputFilename,
                    "https://petstore.swagger.io/v2/swagger.yaml"),
                options.Object);

            var folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(folder);
            var tempFile = Path.Combine(folder, "Petstore.nswag");
            File.WriteAllText(tempFile, contents);

            new NSwagStudioCodeGenerator(tempFile, generalOptions.Object, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            (Code = File.ReadAllText(
                    Path.Combine(
                        Path.GetDirectoryName(tempFile) ?? throw new InvalidOperationException(),
                        $"{outputFilename}.cs")))
                .Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}