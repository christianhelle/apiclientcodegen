using System;
using System.ComponentModel;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Rapicgen.Core;
using Rapicgen.Core.Installer;
using FluentAssertions;
using Moq;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Installer
{
    public class DependencyInstallerTests
    {
        [Fact]
        public void Implements_Interface()
            => typeof(DependencyInstaller)
                .Should()
                .Implement<IDependencyInstaller>();

        [Theory, AutoMoqData]
        public void Requires_INpmInstaller(IFileDownloader downloader)
            => new Action(() => new DependencyInstaller(null, downloader, new ProcessLauncher()))
                .Should()
                .Throw<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Requires_IFileDownloader(INpmInstaller npm)
            => new Action(() => new DependencyInstaller(npm, null, new ProcessLauncher()))
                .Should()
                .Throw<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void InstallAutoRest_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            sut.InstallAutoRest();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("autorest"));
        }

        [Theory, AutoMoqData]
        public void InstallNSwag_When_NSwag_Not_Installed_Invokes_ProcessLauncher(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            mock.Setup(
                    c => c.Start(
                        "nswag",
                        "version",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Throws(new Win32Exception());
            sut.InstallNSwag();
            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global NSwag.ConsoleCore --version 14.4.0",
                    null));
        }

        [Theory, AutoMoqData]
        public void InstallOpenApiGenerator_Invokes_DownloadFile(
            [Frozen] IFileDownloader downloader,
            DependencyInstaller sut)
        {
            sut.InstallOpenApiGenerator();
            Mock.Get(downloader)
                .Verify(
                    c => c.DownloadFile(
                        It.IsAny<string>(),
                        Resource.OpenApiGenerator_SHA1,
                        Resource.OpenApiGenerator_DownloadUrl,
                        false));
        }

        [Theory, AutoMoqData]
        public void InstallSwaggerCodegen_Invokes_DownloadFile(
            [Frozen] IFileDownloader downloader,
            DependencyInstaller sut)
        {
            sut.InstallSwaggerCodegen();
            Mock.Get(downloader)
                .Verify(
                    c => c.DownloadFile(
                        "swagger-codegen-cli.jar",
                        Resource.SwaggerCodegenCli_SHA1,
                        Resource.SwaggerCodegenCli_DownloadUrl,
                        false));
        }
        
        [Theory, AutoMoqData]
        public void InstallKiota_Invokes_ProcessLauncher(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            mock.Setup(
                    c => c.Start(
                        "kiota",
                        "--version",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Throws(new Win32Exception());
            sut.InstallKiota();
            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global Microsoft.OpenApi.Kiota --version 1.29.0",
                    null));
        }
        
        [Theory, AutoMoqData]
        public void InstallKiota_Ignores_ProcessLauncherException_For_Already_Installed(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            Mock.Get(processLauncher)
                .Setup(c => c.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(
                    new ProcessLaunchException(
                        "dotnet",
                        "tool install --global Microsoft.OpenApi.Kiota --version 1.29.0",
                        null,
                        string.Empty,
                        "Tool 'microsoft.openapi.kiota' is already installed."));

            new Action(sut.InstallKiota)
                .Should()
                .NotThrow();
        }
        
        [Theory, AutoMoqData]
        public void InstallKiota_Throw_ProcessLauncherException(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut,
            ProcessLaunchException exception)
        {
            var mock = Mock.Get(processLauncher);
            mock.Setup(
                    c => c.Start(
                        "kiota",
                        "--version",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Throws(new Win32Exception());

            Mock.Get(processLauncher)
                .Setup(c => c.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(exception);

            new Action(sut.InstallKiota)
                .Should()
                .ThrowExactly<ProcessLaunchException>();
        }

        [Theory, AutoMoqData]
        public void InstallRefitter_When_Refitter_Not_Installed_Invokes_ProcessLauncher_To_Install(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            // Setup the tool list command to return output without refitter
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        "tool list --global",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Callback<string, string, Action<string>, Action<string>, string>(
                    (cmd, args, onOutput, onError, workingDir) =>
                    {
                        onOutput?.Invoke("Package Id      Version      Commands");
                        onOutput?.Invoke("other-tool      1.0.0        other");
                    });

            sut.InstallRefitter();

            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global refitter --version 1.6.5",
                    null),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public void InstallRefitter_When_Refitter_Installed_With_Old_Version_Invokes_ProcessLauncher_To_Install(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            // Setup the tool list command to return output with old refitter version
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        "tool list --global",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Callback<string, string, Action<string>, Action<string>, string>(
                    (cmd, args, onOutput, onError, workingDir) =>
                    {
                        onOutput?.Invoke("Package Id      Version      Commands");
                        onOutput?.Invoke("refitter        1.5.0        refitter");
                    });

            sut.InstallRefitter();

            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global refitter --version 1.6.5",
                    null),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public void InstallRefitter_When_Refitter_Installed_With_Current_Version_Does_Not_Install(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            // Setup the tool list command to return output with current refitter version
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        "tool list --global",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Callback<string, string, Action<string>, Action<string>, string>(
                    (cmd, args, onOutput, onError, workingDir) =>
                    {
                        onOutput?.Invoke("Package Id      Version      Commands");
                        onOutput?.Invoke("refitter        1.6.5        refitter");
                    });

            sut.InstallRefitter();

            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global refitter --version 1.6.5",
                    null),
                Times.Never);
        }

        [Theory, AutoMoqData]
        public void InstallRefitter_When_Refitter_Installed_With_Newer_Version_Does_Not_Install(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            // Setup the tool list command to return output with newer refitter version
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        "tool list --global",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Callback<string, string, Action<string>, Action<string>, string>(
                    (cmd, args, onOutput, onError, workingDir) =>
                    {
                        onOutput?.Invoke("Package Id      Version      Commands");
                        onOutput?.Invoke("refitter        1.7.0        refitter");
                    });

            sut.InstallRefitter();

            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global refitter --version 1.6.5",
                    null),
                Times.Never);
        }

        [Theory, AutoMoqData]
        public void InstallRefitter_When_DotNet_Tool_List_Fails_Invokes_ProcessLauncher_To_Install(
            [Frozen] IProcessLauncher processLauncher,
            DependencyInstaller sut)
        {
            var mock = Mock.Get(processLauncher);
            // Setup the tool list command to throw exception
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        "tool list --global",
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        default))
                .Throws(new Win32Exception());

            sut.InstallRefitter();

            mock.Verify(
                c => c.Start(
                    It.IsAny<string>(),
                    "tool install --global refitter --version 1.6.5",
                    null),
                Times.Once);
        }
    }
}
