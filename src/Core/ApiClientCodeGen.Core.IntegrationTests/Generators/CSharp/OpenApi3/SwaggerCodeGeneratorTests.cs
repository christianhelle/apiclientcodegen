using System;
using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.OpenApi3;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.CSharp.OpenApi3
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class SwaggerCodeGeneratorTests : IClassFixture<SwaggerCodeGeneratorFixture>
    {
        private readonly SwaggerCodeGeneratorFixture fixture;

        public SwaggerCodeGeneratorTests(SwaggerCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [SkippableFact(typeof(NotSupportedException))]
        public void Swagger_Generated_Code_NotNullOrWhitespace()
            => fixture.Code.Should().NotBeNullOrWhiteSpace();

        [SkippableFact(typeof(NotSupportedException))]
        public void Swagger_Reports_Progres()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [SkippableFact(typeof(NotSupportedException))]
        public void Reads_JavaPath_From_Options()
            => fixture.OptionsMock.Verify(c => c.JavaPath);

        [SkippableFact(typeof(NotSupportedException))]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetCoreApp,
                    fixture.Code,
                    SupportedCodeGenerator.Swagger)
                .Should()
                .BeTrue();

        [SkippableFact(typeof(NotSupportedException))]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                    ProjectTypes.DotNetStandardLibrary,
                    fixture.Code,
                    SupportedCodeGenerator.Swagger)
                .Should()
                .BeTrue();
    }
}