using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class DependencyDownloaderTests
    {
        [TestMethod]
        public void InstallOpenApiGenerator_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator()
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void InstallSwaggerCodegenCli_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli()
                .Should()
                .NotBeNullOrWhiteSpace();
                
        [TestMethod]
        public void InstallOpenApiGenerator_With_Path_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator(Path.GetTempPath())
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void InstallSwaggerCodegenCli_With_Path_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli(Path.GetTempPath())
                .Should()
                .NotBeNullOrWhiteSpace();
        
        [TestMethod]
        public void InstallOpenApiGenerator_Force_Returns_Path()
            => DependencyDownloader
                .InstallOpenApiGenerator(forceDownload: true)
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void InstallSwaggerCodegenCli_Force_Returns_Path()
            => DependencyDownloader
                .InstallSwaggerCodegenCli(forceDownload: true)
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void InstallAutoRest_Returns_Path()
            => new Action(DependencyDownloader.InstallAutoRest)
                .Should()
                .NotThrow();

        [TestMethod]
        public void InstallNSwag_Returns_Path()
            => new Action(DependencyDownloader.InstallNSwag)
                .Should()
                .NotThrow();
    }
}
