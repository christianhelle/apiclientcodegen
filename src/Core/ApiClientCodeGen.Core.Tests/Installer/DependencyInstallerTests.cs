using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Moq;
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
            => new Action(() => new DependencyInstaller(null, downloader))
                .Should()
                .Throw<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Requires_IFileDownloader(INpmInstaller npm)
            => new Action(() => new DependencyInstaller(npm, null))
                .Should()
                .Throw<ArgumentNullException>();

        [Theory, AutoMoqData]
        public async Task InstallAutoRest_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            await sut.InstallAutoRest();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("autorest"));
        }

        [Theory, AutoMoqData]
        public async Task InstallNSwag_Invokes_Npm(
            [Frozen] INpmInstaller npm,
            DependencyInstaller sut)
        {
            await sut.InstallNSwag();
            Mock.Get(npm)
                .Verify(c => c.InstallNpmPackage("nswag"));
        }

        [Theory, AutoMoqData]
        public async Task InstallOpenApiGenerator_Invokes_DownloadFile(
            [Frozen] IFileDownloader downloader,
            DependencyInstaller sut)
        {
            await sut.InstallOpenApiGenerator();
            Mock.Get(downloader)
                .Verify(
                    c => c.DownloadFile(
                        It.IsAny<string>(),
                        "openapi-generator-cli.jar",

        [Theory, AutoMoqData]
        public async Task InstallSwaggerCodegen_Invokes_DownloadFile(
            [Frozen] IFileDownloader downloader,
            DependencyInstaller sut)
        {
            await sut.InstallSwaggerCodegen();
            Mock.Get(downloader)
                .Verify(
                    c => c.DownloadFile(
                        It.IsAny<string>(),
                        "swagger-codegen-cli.jar",
                        Resource.SwaggerCodegenCli_MD5,
                        Resource.SwaggerCodegenCli_DownloadUrl,
                        false));
        }
    }
}