using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorFixture : TestWithResources
    {
        private static Mock<IGeneralOptions> optionsMock;
        private static IGeneralOptions options;
        public string Code { get; }

        public NSwagStudioCodeGeneratorFixture()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;

            var contents = NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    new EnterOpenApiSpecDialogResult(File.ReadAllText(SwaggerJsonFilename), "Swagger", "https://petstore.swagger.io/v2/swagger.json"))
                .GetAwaiter()
                .GetResult();

            File.WriteAllText(SwaggerNSwagFilename, contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath(SwaggerNSwagFilename), options, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            Code = File.ReadAllText(Path.GetFullPath("PetstoreClient.cs"));
        }
    }
}