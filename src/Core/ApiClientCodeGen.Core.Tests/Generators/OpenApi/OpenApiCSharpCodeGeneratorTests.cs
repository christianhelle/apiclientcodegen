using System;
using System.IO;
using ApiClientCodeGen.Tests.Common;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Moq;
using Rapicgen.Core;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
using Rapicgen.Core.Extensions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.OpenApi
{
    public class OpenApiCSharpCodeGeneratorTests : TestWithResources
    {
        [Theory, AutoMoqData]
        public void Updates_Progress(
            [Frozen] IProgressReporter progressReporter,
            OpenApiCSharpCodeGenerator sut)
        {
            sut.GenerateCode(progressReporter);
            Mock.Get(progressReporter)
                .Verify(
                    c => c.Progress(
                        It.IsAny<uint>(),
                        It.IsAny<uint>()),
                    Times.Exactly(5));
        }

        [Fact]
        public void GenerateCode_Uses_Explicit_Configuration_File_When_Specified()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDirectory);

            try
            {
                var swaggerFile = Path.Combine(tempDirectory, "petstore.yaml");
                File.WriteAllText(swaggerFile, "openapi: 3.0.0");

                var configFile = Path.Combine(tempDirectory, "petstore.config.yaml");
                File.WriteAllText(configFile, "generatorName: csharp");

                var version = OpenApiSupportedVersion.V7230;
                var jarFile = Path.Combine(Path.GetTempPath(), $"openapi-generator-cli-{version.GetDescription()}.jar");
                File.WriteAllText(jarFile, "jar");

                var processLauncher = new Mock<IProcessLauncher>();
                var dependencyInstaller = new Mock<IDependencyInstaller>();
                var options = new DefaultGeneralOptions();
                var openApiGeneratorOptions = new DefaultOpenApiGeneratorOptions
                {
                    Version = version,
                    ConfigurationFile = configFile,
                    UseConfigurationFile = false
                };

                var sut = new OpenApiCSharpCodeGenerator(
                    swaggerFile,
                    "GeneratedCode",
                    options,
                    openApiGeneratorOptions,
                    processLauncher.Object,
                    dependencyInstaller.Object);

                sut.GenerateCode(null);

                processLauncher.Verify(
                    p => p.Start(
                        It.IsAny<string>(),
                        It.Is<string>(arguments => arguments.Contains($"-c \"{configFile}\"")),
                        It.IsAny<string?>()),
                    Times.Once);
            }
            finally
            {
                Directory.Delete(tempDirectory, recursive: true);
                if (File.Exists(Path.Combine(Path.GetTempPath(), $"openapi-generator-cli-{OpenApiSupportedVersion.V7230.GetDescription()}.jar")))
                {
                    File.Delete(Path.Combine(Path.GetTempPath(), $"openapi-generator-cli-{OpenApiSupportedVersion.V7230.GetDescription()}.jar"));
                }
            }
        }
    }
}
