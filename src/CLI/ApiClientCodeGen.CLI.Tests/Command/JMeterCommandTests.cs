using System;
using System.IO;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Moq;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class JMeterCommandTests
    {
        [Theory, AutoMoqData]
        public void OutputPath_Should_NotBeNullOrWhiteSpace(JMeterCommand.Settings settings)
            => settings.OutputPath.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(JMeterCommand.Settings settings)
            => settings.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(JMeterCommand.Settings settings)
            => settings.OutputPath.Should().NotBeNullOrWhiteSpace();        [Theory, AutoMoqData]
        public void Execute_Should_NotThrow(JMeterCommand sut, JMeterCommand.Settings settings)
        {
            settings.OutputPath = Directory.GetCurrentDirectory();
            new Func<int>(() => sut.Execute(null, settings)).Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public void Execute_Should_Create_Generator(
            [Frozen] IJMeterCodeGeneratorFactory factory,
            JMeterCommand sut,
            JMeterCommand.Settings settings)
        {
            settings.OutputPath = Directory.GetCurrentDirectory();
            sut.Execute(null, settings);

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        settings.SwaggerFile,
                        settings.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }

        [Theory, AutoMoqData]
        public void Execute_Should_Write_To_IConsole(
            [Frozen] IJMeterCodeGeneratorFactory factory,
            JMeterCommand sut,
            JMeterCommand.Settings settings)
        {
            var path = Directory.CreateDirectory(
                Path.Combine(
                    Path.GetTempPath(),
                    Guid.NewGuid().ToString("N")));

            settings.OutputPath = path.FullName;
            settings.SkipLogging = false;
            sut.Execute(null, settings);

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        settings.SwaggerFile,
                        settings.OutputPath,
                        It.IsAny<IGeneralOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IDependencyInstaller>()));
        }
    }
}