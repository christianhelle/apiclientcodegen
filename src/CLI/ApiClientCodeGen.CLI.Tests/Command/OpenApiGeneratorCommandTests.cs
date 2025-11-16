using System;
using System.IO;
using System.Threading;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class OpenApiGeneratorCommandTests
    {
        [Theory, AutoMoqData]
        public void OutputPath_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommandSettings settings)
            => settings.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommandSettings settings)
            => settings.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommandSettings settings)
            => settings.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void Execute_Should_NotThrow(OpenApiGeneratorCommand sut, OpenApiGeneratorCommandSettings settings)
        {
            settings.OutputPath = Directory.GetCurrentDirectory();
            new Func<int>(() => sut.Execute(null, settings, CancellationToken.None)).Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public void Execute_Should_Create_Generator(
            [Frozen] IOpenApiGeneratorFactory factory,
            OpenApiGeneratorCommand sut,
            OpenApiGeneratorCommandSettings settings)
        {
            settings.OutputPath = Directory.GetCurrentDirectory();
            sut.Execute(null, settings, CancellationToken.None);

            Mock.Get(factory)
                .Verify(c => c.Create(
                    settings.Generator,
                    settings.SwaggerFile,
                    settings.OutputPath,
                    It.IsAny<IGeneralOptions>(), It.IsAny<IProcessLauncher>(),
                    It.IsAny<IDependencyInstaller>()));
        }

        [Theory, AutoMoqData]
        public void Execute_Should_Write_To_IConsole(
            [Frozen] IOpenApiGeneratorFactory factory,
            OpenApiGeneratorCommand sut,
            OpenApiGeneratorCommandSettings settings)
        {
            var path = Directory.CreateDirectory(
                Path.Combine(
                    Path.GetTempPath(),
                    Guid.NewGuid().ToString("N")));

            settings.OutputPath = path.FullName;
            settings.SkipLogging = false;
            sut.Execute(null, settings, CancellationToken.None);

            Mock.Get(factory)
                .Verify(c => c.Create(
                    settings.Generator,
                    settings.SwaggerFile,
                    settings.OutputPath,
                    It.IsAny<IGeneralOptions>(),
                    It.IsAny<IProcessLauncher>(),
                    It.IsAny<IDependencyInstaller>()));
        }
    }
}