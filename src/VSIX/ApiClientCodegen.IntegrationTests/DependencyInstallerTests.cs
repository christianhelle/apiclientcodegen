using System;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using FluentAssertions;
using Xunit;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.IntegrationTests
{
    public class DependencyInstallerTests : TestWithResources
    {
        [Fact]
        public void InstallOpenApiGenerator_Returns_Path()
            => (new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()), new ProcessLauncher())
                    .InstallOpenApiGenerator(OpenApiSupportedVersion.V7120))
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegen_Returns_Path()
            => (new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()), new ProcessLauncher())
                    .InstallSwaggerCodegen())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallAutoRest_Returns_Path()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()), new ProcessLauncher()).InstallAutoRest())
                .Should()
                .NotThrow();
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public void InstallNSwag_Returns_Path()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()), new ProcessLauncher()).InstallNSwag())
                .Should()
                .NotThrow();
        }
    }
}
