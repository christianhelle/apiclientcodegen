using System;
using System.IO;
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
        public void OutputPath_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommand sut)
            => sut.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(OpenApiGeneratorCommand sut)
            => sut.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(OpenApiGeneratorCommand sut)
        {
            sut.OutputPath = Directory.GetCurrentDirectory();
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public void OnExecute_Should_Create_Generator(
            [Frozen] IOpenApiGeneratorFactory factory,
            OpenApiGeneratorCommand sut)
        {
            sut.OutputPath = Directory.GetCurrentDirectory();
            sut.OnExecute();

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        sut.Generator,
                        sut.SwaggerFile,
                        sut.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }

        [Theory, AutoMoqData]
        public void OnExecute_Should_Write_To_IConsole(
            [Frozen] IOpenApiGeneratorFactory factory,
            OpenApiGeneratorCommand sut)
        {
            var path = Directory.CreateDirectory(
                Path.Combine(
                    Path.GetTempPath(),
                    Guid.NewGuid().ToString("N")));

            sut.OutputPath = path.FullName;
            sut.SkipLogging = false;
            sut.OnExecute();

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        sut.Generator,
                        sut.SwaggerFile,
                        sut.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }
    }
}