using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    public class NSwagStudioCodeGeneratorFixture : TestWithResources
    {
        private readonly Mock<IGeneralOptions> optionsMock;
        private readonly IGeneralOptions options;
        public string Code { get; }

        public NSwagStudioCodeGeneratorFixture()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;

            var contents = NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    new EnterOpenApiSpecDialogResult(File.ReadAllText(SwaggerV3JsonFilename), "PetstoreClient", "https://petstore.swagger.io/v2/swagger.json"))
                .GetAwaiter()
                .GetResult();

            File.WriteAllText(SwaggerV3NSwagFilename, contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath(SwaggerV3NSwagFilename), options, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            Code = File.ReadAllText(Path.GetFullPath("PetstoreClient.cs"));
        }
    }
}