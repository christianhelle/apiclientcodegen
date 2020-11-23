using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    public class AutoRestCodeGeneratorFixture : TestWithResources, IDisposable
    {
        public readonly Mock<IProgressReporter> ProgressReporterMock = new Mock<IProgressReporter>();
        public readonly Mock<IAutoRestOptions> OptionsMock = new Mock<IAutoRestOptions>();
        public readonly string Code;

        public AutoRestCodeGeneratorFixture()
        {
            OptionsMock.Setup(c => c.AddCredentials).Returns(true);
            OptionsMock.Setup(c => c.UseDateTimeOffset).Returns(true);
            OptionsMock.Setup(c => c.UseInternalConstructors).Returns(true);

            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath(SwaggerJsonFilename),
                "GeneratedCode",
                OptionsMock.Object,
                new ProcessLauncher(),
                new OpenApiDocumentFactory());

            OptionsMock.Setup(c => c.OverrideClientName).Returns(true);
            Code = codeGenerator.GenerateCode(ProgressReporterMock.Object);
        }

        public void Dispose()
            => DependencyUninstaller.UninstallAutoRest();
    }
}