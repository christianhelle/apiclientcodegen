using System;
using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagCodeGeneratorTests : IClassFixture<NSwagCodeGeneratorFixture>
    {
        private readonly NSwagCodeGeneratorFixture fixture;

        public NSwagCodeGeneratorTests(NSwagCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void NSwag_Generated_Code_NotNullOrWhitespace()
            => fixture.Code.Should().NotBeNullOrWhiteSpace();

        [Fact]
        public void NSwag_Reports_Progres()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [Fact]
        public void Reads_InjectHttpClient_From_Options()
            => fixture.OptionsMock.Verify(c => c.InjectHttpClient);

        [Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => fixture.OptionsMock.Verify(c => c.GenerateClientInterfaces);

        [Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => fixture.OptionsMock.Verify(c => c.GenerateDtoTypes);

        [Fact]
        public void Reads_UseBaseUrl_From_Options()
            => fixture.OptionsMock.Verify(c => c.UseBaseUrl);

        [Fact]
        public void Reads_ClassStyle_From_Options()
            => fixture.OptionsMock.Verify(c => c.ClassStyle);

        [Fact]
        public void Reads_UseDocumentTitle_From_Options()
            => fixture.OptionsMock.Verify(c => c.UseDocumentTitle);

        [Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetCoreApp,
                fixture.Code,
                SupportedCodeGenerator.NSwag);

        [Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(
                ProjectTypes.DotNetStandardLibrary,
                fixture.Code,
                SupportedCodeGenerator.NSwag);
    }
}
