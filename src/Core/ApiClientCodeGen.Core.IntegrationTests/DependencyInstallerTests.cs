﻿using System;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
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
                        new FileDownloader(new WebDownloader()),
                        new ProcessLauncher())
                    .InstallOpenApiGenerator())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegen_Returns_Path()
            => (new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()),
                        new ProcessLauncher())
                    .InstallSwaggerCodegen())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallAutoRest_Does_NotThrow()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()),
                        new ProcessLauncher()).InstallAutoRest())
                .Should()
                .NotThrow();
        }

        [SkippableFact(typeof(ProcessLaunchException))]
        public void InstallNSwag_Does_NotThrow()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()),
                        new ProcessLauncher()).InstallNSwag())
                .Should()
                .NotThrow();
        }

        [Fact]
        public void InstallKiota_Does_NotThrow()
        {
            new Action(
                    () => new DependencyInstaller(
                        new NpmInstaller(new ProcessLauncher()),
                        new FileDownloader(new WebDownloader()),
                        new ProcessLauncher()).InstallKiota())
                .Should()
                .NotThrow();
        }
    }
}