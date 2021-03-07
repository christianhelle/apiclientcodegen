using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    public class DependencyInstallerTests : TestWithResources
    {
        [Fact]
        public async Task InstallOpenApiGenerator_Returns_Path_Async()
            => (await new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()))
                    .InstallOpenApiGenerator())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public async Task InstallSwaggerCodegen_Returns_Path_Async()
            => (await new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()))
                    .InstallSwaggerCodegen())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public async Task InstallAutoRest_Returns_Path_Async()
        {
            var func = new Func<Task>(
                () => new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())).InstallAutoRest());
            await func();
            func.Should().NotThrow();
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public async Task InstallNSwag_Returns_Path_Async()
        {
            var func = new Func<Task>(
                () => new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader())).InstallNSwag());
            await func();
            func.Should().NotThrow();
        }
    }
}