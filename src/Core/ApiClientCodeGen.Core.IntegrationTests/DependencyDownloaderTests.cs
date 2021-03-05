using System;
using System.IO;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    public class DependencyDownloaderTests : TestWithResources
    {
        [Fact]
        public void InstallOpenApiGenerator_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator()
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegenCli_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli()
                .Should()
                .NotBeNullOrWhiteSpace();
                
        [Fact]
        public void InstallOpenApiGenerator_With_Path_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator(Path.GetTempPath())
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegenCli_With_Path_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli(Path.GetTempPath())
                .Should()
                .NotBeNullOrWhiteSpace();
        
        [Fact]
        public void InstallOpenApiGenerator_Force_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator(forceDownload: true)
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallSwaggerCodegenCli_Force_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli(forceDownload: true)
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void InstallAutoRest_Returns_Path()
            => new Action(DependencyDownloader.InstallAutoRest)
                .Should()
                .NotThrow();

        [SkippableFact(typeof(ProcessLaunchException))]
        public void InstallNSwag_Returns_Path()
            => new Action(DependencyDownloader.InstallNSwag)
                .Should()
                .NotThrow();
    }
}
