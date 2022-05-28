using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    public class DependencyInstallerTests : TestWithResources
    {
        [Fact]
        public void InstallOpenApiGenerator_Returns_Path()
            => (new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()))
                    .InstallOpenApiGenerator())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegen_Returns_Path()
            => (new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()))
                    .InstallSwaggerCodegen())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallAutoRest_Returns_Path()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader())).InstallAutoRest())
                .Should()
                .NotThrow();
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public void InstallNSwag_Returns_Path()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader())).InstallNSwag())
                .Should()
                .NotThrow();
        }
    }
}