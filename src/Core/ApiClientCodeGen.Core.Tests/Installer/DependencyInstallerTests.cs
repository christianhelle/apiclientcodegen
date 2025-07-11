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
        public void InstallNSwag_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            sut.InstallNSwag();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("nswag"));
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
                    "tool install --global Microsoft.OpenApi.Kiota --version 1.28.0",
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
                        "tool install --global Microsoft.OpenApi.Kiota --version 1.28.0",
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
    }
}
