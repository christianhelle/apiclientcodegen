using System;
using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.Yaml;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class SwaggerCodeGeneratorYamlTests : IClassFixture<SwaggerCodeGeneratorFixture>
    {
        private readonly SwaggerCodeGeneratorFixture fixture;

        public SwaggerCodeGeneratorYamlTests(SwaggerCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void Swagger_Generated_Code_NotNullOrWhitespace()
            => fixture.Code.Should().NotBeNullOrWhiteSpace();

        [Fact]
        public void Swagger_Reports_Progres()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Fact]
        public void Reads_JavaPath_From_Options()
            => fixture.OptionsMock.Verify(c => c.JavaPath);

        [Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetCoreApp,
                    fixture.Code,
                    SupportedCodeGenerator.Swagger)
                .Should()
                .BeTrue();

        [Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetStandardLibrary,
                    fixture.Code,
                    SupportedCodeGenerator.Swagger)
                .Should()
                .BeTrue();
    }
}