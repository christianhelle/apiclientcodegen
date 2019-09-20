using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
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
        public void InstallAutoRest_Returns_Path()
            => new Action(DependencyDownloader.InstallAutoRest)
                .Should()
                .NotThrow();
    }
}
