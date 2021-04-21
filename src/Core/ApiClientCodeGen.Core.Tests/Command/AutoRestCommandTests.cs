using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Command
{
    public class AutoRestCommandTests
    {
        [Theory, AutoMoqData]
        public void DefaultNamespace_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.DefaultNamespace.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void SwaggerFile_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.SwaggerFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void OutputFile_Should_NotBeNullOrWhiteSpace(AutoRestCommand sut)
            => sut.OutputFile.Should().NotBeNullOrWhiteSpace();

        [Theory, AutoMoqData]
        public void CreateGenerator_Should_NotNull(AutoRestCommand sut)
            => sut.CreateGenerator().Should().NotBeNull();

        [Theory, AutoMoqData]
        public void OnExecuteAsync_Should_NotThrow(AutoRestCommand sut)
        {
            new Func<int>(sut.OnExecute).Should().NotThrow();
        }

        [Theory, AutoMoqData]
        public void Constructor_Should_Throw_On_Null_IAutoRestOptions(
            IConsoleOutput console,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            => new Action(
                    () => new AutoRestCommand(
                        console,
                        null,
                        processLauncher,
                        progressReporter,
                        factory,
                        documentFactory,
                        dependencyInstaller))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Should_Throw_On_Null_IProcessLauncher(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProgressReporter progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            => new Action(
                    () => new AutoRestCommand(
                        console,
                        options,
                        null,
                        progressReporter,
                        factory,
                        documentFactory,
                        dependencyInstaller))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Should_Throw_On_Null_IAutoRestCodeGeneratorFactory(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter,
            IOpenApiDocumentFactory documentFactory,
            IDependencyInstaller dependencyInstaller)
            => new Action(
                    () => new AutoRestCommand(
                        console,
                        options,
                        processLauncher,
                        progressReporter,
                        null,
                        documentFactory,
                        dependencyInstaller))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Constructor_Should_Throw_On_Null_IOpenApiDocumentFactory(
            IConsoleOutput console,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IProgressReporter progressReporter,
            IAutoRestCodeGeneratorFactory factory,
            IDependencyInstaller dependencyInstaller)
            => new Action(
                    () => new AutoRestCommand(
                        console,
                        options,
                        processLauncher,
                        progressReporter,
                        factory,
                        null,
                        dependencyInstaller))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void OnExecute_Should_Create_Generator(
            [Frozen] IAutoRestCodeGeneratorFactory factory,
            AutoRestCommand sut,
            string swaggerFile)
        {
            sut.SwaggerFile = swaggerFile;
            sut.OnExecute();

            Mock.Get(factory)
                .Verify(
                    c => c.Create(
                        swaggerFile,
                        "GeneratedCode",
                        It.IsAny<IAutoRestOptions>(),
                        It.IsAny<IProcessLauncher>(),
                        It.IsAny<IOpenApiDocumentFactory>(),
                        It.IsAny<IDependencyInstaller>()));
        }
    }
}