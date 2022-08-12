using System;
using System.IO;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CLI.Tests.Command
{
    public class JMeterCommandTests
    {
        [Theory, AutoMoqData]
        public void OutputPath_Should_NotBeNullOrWhiteSpace(JMeterCommand sut)
            => sut.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(JMeterCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(JMeterCommand sut)
            => sut.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(JMeterCommand sut)
        {
            sut.OutputPath = Directory.GetCurrentDirectory();
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public void OnExecute_Should_Create_Generator(
            [Frozen] IJMeterCodeGeneratorFactory factory,
            JMeterCommand sut)
        {
            sut.OutputPath = Directory.GetCurrentDirectory();
            sut.OnExecute();

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        sut.SwaggerFile,
                        sut.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }

        [Theory, AutoMoqData]
        public void OnExecute_Should_Write_To_IConsole(
            [Frozen] IJMeterCodeGeneratorFactory factory,
            JMeterCommand sut)
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
                        sut.SwaggerFile,
                        sut.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }
    }
}